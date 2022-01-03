using Marketer.Infrastructure.EfCore;
using Marketer.Query.Queries.Settings;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Query.Commands
{
    public class SettingQuery : ISettingQuery
    {
        private readonly MarketerContext _context;

        public SettingQuery(MarketerContext context) => _context = context;

        public async Task<SettingQueryVM> Get() => await _context.Settings.Select(s => new SettingQueryVM
        {
            Emails = s.Emails,
            Mobiles = s.Mobiles,
            SummaryText = s.SummaryText,
            Text = s.Text
        }).FirstOrDefaultAsync();

        public async Task<string> GetSummaryText() => await _context.Settings.Select(s => s.SummaryText).FirstOrDefaultAsync();

        public async Task<string> GetAboutUsTextAsync() => await _context.Settings.Select(s => s.Text).FirstOrDefaultAsync();
    }
}