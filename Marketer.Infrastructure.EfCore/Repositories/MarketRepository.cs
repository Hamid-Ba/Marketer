using Framework.Infrastructure;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Domain.Entities.Products;
using Marketer.Domain.RI.Products;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Infrastructure.EfCore.Repositories
{
    public class MarketRepository : Repository<Market>, IMarketRepository
    {
        private readonly MarketerContext _context;

        public MarketRepository(MarketerContext context) : base(context) => _context = context;

        public async Task<IEnumerable<MarketVM>> GetAll()
        {
            var visitor = await _context.Visitors.Select(v => new { Id = v.Id, Name = v.FullName }).ToListAsync();

            var markets = await _context.Markets.Include(c => c.City).Select(m => new MarketVM
            {
                Id = m.Id,
                CityId = m.CityId,
                VisitorId = m.VisitorId,
                Name = m.Name,
                CityName = m.City.Name,
                Owner = m.Owner,
                MobilePhone = m.MobilePhone,
                Address = m.Address
            }).AsNoTracking().ToListAsync();

            markets.ForEach(m => m.VisitorName = visitor.Find(v => v.Id == m.VisitorId)?.Name);

            return markets;
        }

        public async Task<IEnumerable<MarketVM>> GetAll(long visitorId)
        {
            var visitor = await _context.Visitors.Select(v => new { Id = v.Id, Name = v.FullName }).ToListAsync();

            var markets = await _context.Markets.Where(v => v.VisitorId == visitorId).Include(c => c.City).Select(m => new MarketVM
            {
                Id = m.Id,
                CityId = m.CityId,
                VisitorId = m.VisitorId,
                Name = m.Name,
                CityName = m.City.Name,
                Owner = m.Owner,
                MobilePhone = m.MobilePhone,
                Address = m.Address
            }).AsNoTracking().ToListAsync();

            markets.ForEach(m => m.VisitorName = visitor.Find(v => v.Id == m.VisitorId)?.Name);

            return markets;
        }

        public async Task<MarketVM> GetBy(long id) => await _context.Markets.Select(m => new MarketVM
        {
            Id = m.Id,
            VisitorId = m.VisitorId,
            VisitorName = m.Visitor.FullName,
            CityId = m.CityId,
            CityName = m.City.Name,
            Name = m.Name,
            MobilePhone = m.MobilePhone,
            Owner = m.Owner,
            Address = m.Address
        }).FirstOrDefaultAsync(m => m.Id == id);

        public async Task<EditMarketVM> GetDetailForEditBy(long id) => await _context.Markets.Select(m => new EditMarketVM
        {
            Id = m.Id,
            CityId = m.CityId,
            Name = m.Name,
            MobilePhone = m.MobilePhone,
            Owner = m.Owner,
            VisitorId = m.VisitorId,
            Address = m.Address
        }).FirstOrDefaultAsync(m => m.Id == id);

    }
}