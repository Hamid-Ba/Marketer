using Framework.Infrastructure;
using Marketer.Application.Contract.ViewModels.Extera;
using Marketer.Domain.Entities.Extera;
using Marketer.Domain.RI.Extera;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Infrastructure.EfCore.Repositories
{
    public class SettingRepository : Repository<Setting>, ISettingRepository
    {
        private readonly MarketerContext _context;

        public SettingRepository(MarketerContext context) : base(context) => _context = context;

        public async Task<Setting> GetEntity() => await _context.Settings.FirstOrDefaultAsync();

        public async Task<SettingVM> GetSetting() => await _context.Settings.Select(s =>
            new SettingVM()
            {
                Text = s.Text,
                Emails = s.Emails,
                Mobiles = s.Mobiles
            }).FirstOrDefaultAsync();

    }
}