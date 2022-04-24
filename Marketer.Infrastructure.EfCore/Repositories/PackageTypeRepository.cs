using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Domain.RI.Products;
using Microsoft.EntityFrameworkCore;

namespace Marketer.Infrastructure.EfCore.Repositories
{
    public class PackageTypeRepository : Repository<Marketer.Domain.Entities.Products.PackageType>, IPackageTypeRepository
    {
        private readonly MarketerContext _context;

        public PackageTypeRepository(MarketerContext context) : base(context) => _context = context;

        public async Task<IEnumerable<PackageTypeVM>> GetAll() => await _context.PackageTypes.Select(p => new PackageTypeVM
        {
            Id = p.Id,
            Title = p.Title
        }).AsNoTracking().ToListAsync();


        public async Task<PackageTypeVM> GetBy(long id) => await _context.PackageTypes.Select(p => new PackageTypeVM
        {
            Id = p.Id,
            Title = p.Title
        }).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);


        public async Task<EditPackageTypeVM> GetDetailForEditBy(long id) => await _context.PackageTypes.Select(p => new EditPackageTypeVM
        {
            Id = p.Id,
            Title = p.Title
        }).FirstOrDefaultAsync(p => p.Id == id);

    }
}