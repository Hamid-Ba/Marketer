using Framework.Infrastructure;
using Marketer.Application.Contract.ViewModels.Orders;
using Marketer.Domain.Entities.Orders;
using Marketer.Domain.RI.Orders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Infrastructure.EfCore.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly MarketerContext _context;

        public OrderRepository(MarketerContext context) : base(context) => _context = context;

        public async Task<IEnumerable<OrderVM>> GetAll() => await _context.Orders
            .Include(v => v.Visitor)
            .Include(m => m.Market)
            .Where(p => p.IsPayed)
            .Select(o => new OrderVM
            {
                Id = o.Id,
                MarketId = o.MarketId,
                MarketName = o.Market.Name,
                VisitorId = o.VisitorId,
                VisitorName = o.Visitor.FullName,
                PayAmount = o.PayAmount,
                TotalPrice = o.TotalPrice,
                TotalDiscount = o.TotalDiscount,
                RefId = o.RefId,
                PlaceOrderDate = o.PlaceOrderDate,
                Status = o.Status
            }).OrderByDescending(o => o.Id).AsNoTracking().ToListAsync();

        public async Task<IEnumerable<OrderVM>> GetAllBy(long visitorId) => await _context.Orders
            .Include(v => v.Visitor)
            .Include(m => m.Market)
            .Where(p => p.IsPayed && p.VisitorId == visitorId)
            .Select(o => new OrderVM
            {
                Id = o.Id,
                MarketId = o.MarketId,
                MarketName = o.Market.Name,
                VisitorId = o.VisitorId,
                VisitorName = o.Visitor.FullName,
                PayAmount = o.PayAmount,
                TotalPrice = o.TotalPrice,
                TotalDiscount = o.TotalDiscount,
                RefId = o.RefId,
                PlaceOrderDate = o.PlaceOrderDate,
                Status = o.Status
            }).OrderByDescending(o => o.Id).ToListAsync();

        public async Task<ChangeStatusOrderVM> GetDetailForChangeStatusBy(long id) => await _context.Orders.Select(o => new ChangeStatusOrderVM
        {
            Id = o.Id,
            Status = o.Status
        }).FirstOrDefaultAsync(o => o.Id == id);

        public async Task<Order> GetOpenOrder(long visitorId) => await _context.Orders.Include(o => o.OrderItems).ThenInclude(p => p.Product)
            .Where(o => !o.IsPayed).FirstOrDefaultAsync(o => o.VisitorId == visitorId);

        public async Task<bool> IsThereOpenOrder(long visitorId) => await _context.Orders.Where(o => o.VisitorId == visitorId).AnyAsync(o => !o.IsPayed);

    }
}