using Marketer.Infrastructure.EfCore;
using Marketer.Query.Queries.Markets;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Query.Commands
{
    public class MarketQuery : IMarketQuery
    {
        private readonly MarketerContext _context;

        public MarketQuery(MarketerContext context) => _context = context;

        public async Task<IEnumerable<MarketQueryVM>> GetAllBy(long visitorId) => await _context.Markets
            .Where(m => m.VisitorId == visitorId)
            .Include(c => c.City)
            .Include(v => v.Visitor)
            .Select(m => new MarketQueryVM
            {
                Id = m.Id,
                CityId = m.CityId,
                CityName = m.City.Name,
                Name = m.Name,
                MobilePhone = m.MobilePhone,
                Owner = m.Owner,
                VisitorCode = m.Visitor.UniqueCode,
                VisitorId = m.VisitorId,
                VisitorName = m.Visitor.FullName,
                Address = m.Address,
                MarketWithCity = $"{m.Name} - {m.City.Name}"
            }).AsNoTracking().ToListAsync();
        
    }
}