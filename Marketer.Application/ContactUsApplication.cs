using Framework.Application;
using Marketer.Application.Contract.AI.Extera;
using Marketer.Application.Contract.ViewModels.Extera;
using Marketer.Domain.Entities.Extera;
using Marketer.Domain.RI.Extera;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application
{
    public class ContactUsApplication : IContactUsApplication
    {
        private readonly IContactUsRepository _contactUsRepository;

        public ContactUsApplication(IContactUsRepository contactUsRepository) => _contactUsRepository = contactUsRepository;

        public async Task<string> GetMessageBy(long id) => await _contactUsRepository.GetMessageBy(id);

        public async Task<IEnumerable<ContactUsVM>> GetAll() => await _contactUsRepository.GetAll();

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var contactUs = await _contactUsRepository.GetEntityByIdAsync(id);
            if (contactUs is null) return result.Failed(ApplicationMessage.NotExist);

            contactUs.Delete();
            await _contactUsRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Send(CreateContactUs command)
        {
            OperationResult result = new();

            if (string.IsNullOrWhiteSpace(command.FullName)) return result.Failed("نام نمی تواند خال باشد");
            if (string.IsNullOrWhiteSpace(command.MobilePhone)) return result.Failed("موبایل خود را وارد نمایید");
            if (string.IsNullOrWhiteSpace(command.Message)) return result.Failed("پیام خود را وارد نمایید");

            var contactUs = new ContactUs(command.FullName, command.MobilePhone, command.Message);

            await _contactUsRepository.AddEntityAsync(contactUs);
            await _contactUsRepository.SaveChangesAsync();

            return result.Succeeded();
        }
    }
}