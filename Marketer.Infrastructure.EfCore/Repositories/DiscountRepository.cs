using Framework.Application;
using Framework.Infrastructure;
using Marketer.Application.Contract.ViewModels.Discounts;
using Marketer.Domain.Entities.Discounts;
using Marketer.Domain.RI.Discounts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Infrastructure.EfCore.Repositories
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        private readonly MarketerContext _context;

        public DiscountRepository(MarketerContext context) : base(context) => _context = context;

        public async Task<IEnumerable<DiscountVM>> GetAll()
        {
            var products = await _context.Products
                .Select(p => new { Id = p.Id, Name = p.Title }).ToListAsync();

            var result = await _context.Discounts.Select(c =>
               new DiscountVM()
               {
                   Id = c.Id,
                   ProductId = c.ProductId,
                   DiscountRate = c.DiscountRate,
                   StartDate = c.StartDate.ToFarsi(),
                   EndDate = c.EndDate.ToFarsi(),
                   Reason = c.Reason
               }).AsNoTracking().ToListAsync();


            foreach (var discount in result)
                discount.ProductName = products.Find(p => p.Id == discount.ProductId)?.Name;

            return result;
        }

        public async Task<EditDiscountVM> GetDetailForEditBy(long id) => await _context.Discounts.Select(d => new EditDiscountVM
        {
            Id = d.Id,
            ProductId = d.ProductId,
            DiscountRate = d.DiscountRate,
            StartDate = d.StartDate.ToFarsi(),
            EndDate = d.EndDate.ToFarsi(),
            Reason = d.Reason
        }).FirstOrDefaultAsync(d => d.Id == id);

    }
}