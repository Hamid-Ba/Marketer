using Framework.Application;
using Marketer.Application.Contract.AI.Orders;
using Marketer.Domain.RI.Orders;
using Marketer.Domain.RI.Products;
using System.Linq;
using System.Threading.Tasks;
using Marketer.Domain.Entities.Orders;

namespace Marketer.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _itemRepository;
        private readonly IProductRepository _productRepository;

        public OrderApplication(IOrderRepository orderRepository, IOrderItemRepository itemRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
            _productRepository = productRepository;
        }

        public async Task<OperationResult> AddProductToOpenOrder(long visitorId, string productSlug)
        {
            OperationResult result = new();

            var product = await _productRepository.GetBy(productSlug);
            if (product is null) return result.Failed(ApplicationMessage.NotExist);


            if (await IsThereOpenOrder(visitorId))
            {
                var openOrder = await _orderRepository.GetOpenOrder(visitorId);

                if (openOrder.OrderItems.Any(o => o.ProductId == product.Id))
                {
                    var item = openOrder.OrderItems.FirstOrDefault(o => o.ProductId == product.Id);

                    //Check Stock
                    if (item.Count + 1 > product.Count)
                        return result.Failed("این تعداد محصول در انبار موجود نمی باشد");


                    item.AddCount(1);
                }
                else
                {
                    var item = new OrderItem(openOrder.Id, product.Id, 1);
                    openOrder.AddItem(item);
                }
            }

            else
            {
                await CreateOrder(visitorId);
                var openOrder = await _orderRepository.GetOpenOrder(visitorId);

                var item = new OrderItem(openOrder.Id, product.Id, 1);
                openOrder.AddItem(item);
            }

            await _orderRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<int> CountOfProductInItem(long orderItemId)
        {
            var item = await _itemRepository.GetEntityByIdAsync(orderItemId);
            return item.Count;
        }


        public async Task<OperationResult> CreateOrder(long visitorId)
        {
            OperationResult result = new();

            var order = new Order(visitorId);

            await _orderRepository.AddEntityAsync(order);
            await _orderRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> DeleteOrderItemBy(long visitorId, long orderItemId)
        {
            OperationResult result = new();

            var item = await _itemRepository.GetBy(orderItemId);

            if (item is null) return result.Failed(ApplicationMessage.NotExist);
            if (item.Order.VisitorId != visitorId) return result.Failed("این سفارش متعلق به شما نمی باشد");

            _itemRepository.DeleteEntity(item);
            await _itemRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<bool> IsThereOpenOrder(long visitorId) => await _orderRepository.IsThereOpenOrder(visitorId);

    }
}