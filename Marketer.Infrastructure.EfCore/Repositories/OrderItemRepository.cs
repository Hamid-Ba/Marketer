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
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private readonly MarketerContext _context;

        public OrderItemRepository(MarketerContext context) : base(context) => _context = context;

        public async Task<OrderItem> GetBy(long id) => await _context.OrderItems.Include(o => o.Order).FirstOrDefaultAsync(o => o.Id == id);

        public async Task<IEnumerable<OrderItemVM>> GetOrderDetails(long orderId) => await _context.OrderItems
            .Where(o => o.OrderId == orderId)
            .Include(o => o.Product)
            .Select(o => new OrderItemVM
            {
                Id = o.Id,
                OrderId = o.OrderId,
                ProductId = o.ProductId,
                ProductTitle = o.Product.Title,
                PayAmount = o.PayAmount,
                DiscountPrice = o.DiscountPrice,
                Count = o.Count
            }).AsNoTracking().ToListAsync();

        public async Task<IEnumerable<OrderItemVM>> GetOrderDetails(long orderId, long visitorId) => await _context.OrderItems
            .Include(o => o.Order)
            .Where(o => o.OrderId == orderId && o.Order.VisitorId == visitorId)
            .Include(o => o.Product)
            .Select(o => new OrderItemVM
            {
                Id = o.Id,
                OrderId = o.OrderId,
                ProductId = o.ProductId,
                ProductTitle = o.Product.Title,
                PayAmount = o.PayAmount,
                DiscountPrice = o.DiscountPrice,
                Count = o.Count
            }).AsNoTracking().ToListAsync();
    }
}