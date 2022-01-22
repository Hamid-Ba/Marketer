using Framework.Infrastructure;
using Marketer.Domain.Entities.Orders;
using Marketer.Domain.RI.Orders;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Marketer.Infrastructure.EfCore.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private readonly MarketerContext _context;

        public OrderItemRepository(MarketerContext context) : base(context) => _context = context;

        public async Task<OrderItem> GetBy(long id) => await _context.OrderItems.Include(o => o.Order).FirstOrDefaultAsync(o => o.Id == id);
        
    }
}