using Framework.Infrastructure;
using Marketer.Domain.Entities.Orders;
using Marketer.Domain.RI.Orders;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Infrastructure.EfCore.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly MarketerContext _context;

        public OrderRepository(MarketerContext context) : base(context) => _context = context;

        public async Task<Order> GetOpenOrder(long visitorId) => await _context.Orders.Include(o => o.OrderItems).Where(o => !o.IsPayed).FirstOrDefaultAsync(o => o.VisitorId == visitorId);

        public async Task<bool> IsThereOpenOrder(long visitorId) => await _context.Orders.Where(o => o.VisitorId == visitorId).AnyAsync(o => o.IsPayed);
        
    }
}