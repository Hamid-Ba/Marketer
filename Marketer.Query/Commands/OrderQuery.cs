using Marketer.Domain.Entities.Orders;
using Marketer.Domain.Entities.Products;
using Marketer.Infrastructure.EfCore;
using Marketer.Query.Queries.Orders;
using Marketer.Query.Queries.Products;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Marketer.Query.Commands
{
    public class OrderQuery : IOrderQuery
    {
        private readonly MarketerContext _context;

        public OrderQuery(MarketerContext context) => _context = context;

        public async Task<OrderQueryVM> GetOpenOrder(long visitorId)
        {
            if (await _context.Orders.AnyAsync(o => !o.IsPayed))
            {
                var discount = await _context.Discounts.Where(d => d.StartDate <= DateTime.Now && DateTime.Now <= d.EndDate).Select(d => new
                {
                    Rate = d.DiscountRate,
                    Id = d.Id,
                    ProductId = d.ProductId
                }).ToListAsync();

                var order = await _context.Orders.Include(o => o.OrderItems).ThenInclude(p => p.Product)
                    .Where(o => !o.IsPayed && o.VisitorId == visitorId).Select(o => new OrderQueryVM
                    {
                        Id = o.Id,
                        VisitorId = o.VisitorId,
                        IsPayed = o.IsPayed,
                        Items = MapItems(o.OrderItems)
                    }).FirstOrDefaultAsync();

                if (order == null) return null;

                order.TotalPrice = order.Items.Sum(o => o.Product.PurchasePrice * o.Count);
                order.ItemsCount = order.Items.Count;
                order.Items.ForEach(o => o.PayAmount = o.Product.PurchasePrice * o.Count);
                foreach (var dis in discount)
                {
                    if (order.Items.Any(i => i.ProductId == dis.ProductId))
                    {
                        var item = order.Items.FirstOrDefault(o => o.ProductId == dis.ProductId);

                        item.DiscountPrice = item.Product.PurchasePrice * dis.Rate / 100;

                        order.TotalDiscount += (item.DiscountPrice * item.Count);
                    }

                }
                order.PayAmount = order.TotalPrice - order.TotalDiscount;

                return order;
            }

            return null;
        }

        private static List<OrderItemQuertVM> MapItems(List<OrderItem> orderItems) => orderItems.Select(o => new OrderItemQuertVM
        {
            Id = o.Id,
            OrderId = o.OrderId,
            ProductId = o.ProductId,
            Count = o.Count,
            DiscountPrice = o.DiscountPrice,
            PayAmount = o.PayAmount,
            Product = MapProduct(o.Product)
        }).ToList();

        private static ProductQueryVM MapProduct(Product product) => new ProductQueryVM
        {
            Id = product.Id,
            Title = product.Title,
            Slug = product.Slug,
            Picture = product.Picture,
            PictureAlt = product.PictureAlt,
            PictureTitle = product.PictureTitle,
            PurchasePrice = product.PurchacePrice
        };

    }
}