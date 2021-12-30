using Framework.Application;
using Framework.Infrastructure;
using Marketer.Application.Contract.ViewModels.Extera;
using Marketer.Domain.Entities.Extera;
using Marketer.Domain.RI.Extera;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Infrastructure.EfCore.Repositories
{
    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
    {
        private readonly MarketerContext _context;

        public ContactUsRepository(MarketerContext context) : base(context) => _context = context;


        public async Task<string> GetMessageBy(long id) => (await _context.ContactUs.FirstOrDefaultAsync(c => c.Id == id)).Message;

        public async Task<IEnumerable<ContactUsVM>> GetAll() => await _context.ContactUs.Select(c => new ContactUsVM()
        {
            Id = c.Id,
            FullName = c.FullName,
            MobilePhone = c.MobilePhone,
            Message = c.Message,
            CreationDate = c.CreationDate.ToFarsi()
        }).AsNoTracking().OrderByDescending(o => o.Id).ToListAsync();
    }
}