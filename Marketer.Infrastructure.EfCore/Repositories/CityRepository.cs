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
    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly MarketerContext _context;

        public CityRepository(MarketerContext context) : base(context) => _context = context;

        public async Task<IEnumerable<CityVM>> GetAll() => await _context.Cities.Select(c => new CityVM
        {
            Id = c.Id,
            Name = c.Name
        }).AsNoTracking().ToListAsync();

        public async Task<EditCityVM> GetDetailForEditBy(long id) => await _context.Cities.Select(c => new EditCityVM
        {
            Id = c.Id,
            Name = c.Name
        }).FirstOrDefaultAsync(c => c.Id == id);

    }
}