using Framework.Application;
using Marketer.Application.Contract.AI.Extera;
using Marketer.Application.Contract.ViewModels.Extera;
using Marketer.Domain.Entities.Extera;
using Marketer.Domain.RI.Extera;
using System.Threading.Tasks;

namespace Marketer.Application
{
    public class SettingApplication : ISettingApplication
    {
        private readonly ISettingRepository _settingRepository;

        public SettingApplication(ISettingRepository settingRepository) => _settingRepository = settingRepository;

        public async Task<SettingVM> GetSetting() => await _settingRepository.GetSetting();

        public async Task<OperationResult> Modify(SettingVM command)
        {
            OperationResult result = new();

            var setting = await _settingRepository.GetEntity();

            if (setting is null)
            {
                var baseSetting = new Setting(command.Mobiles, command.Emails, command.Text);
                await _settingRepository.AddEntityAsync(baseSetting);
            }
            else setting.Edit(command.Mobiles, command.Emails, command.Text);

            await _settingRepository.SaveChangesAsync();
            return result.Succeeded();
        }
    }
}