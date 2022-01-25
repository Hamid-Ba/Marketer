using Framework.Application;
using Marketer.Application.Contract.AI.Orders;
using Marketer.Domain.RI.Orders;
using Marketer.Domain.RI.Products;
using System.Linq;
using System.Threading.Tasks;
using Marketer.Domain.Entities.Orders;
using Marketer.Application.Contract.ViewModels.Orders;
using Marketer.Domain.RI.Discounts;
using System.Collections.Generic;

namespace Marketer.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _itemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDiscountRepository _discountRepository;

        public OrderApplication(IOrderRepository orderRepository, IOrderItemRepository itemRepository, IProductRepository productRepository, IDiscountRepository discountRepository)
        {
            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
            _productRepository = productRepository;
            _discountRepository = discountRepository;
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

        public async Task<OperationResult> ChangeStatus(ChangeStatusOrderVM command)
        {
            OperationResult result = new();

            var order = await _orderRepository.GetEntityByIdAsync(command.Id);
            if (order is null) return result.Failed(ApplicationMessage.NotExist);

            order.ChangeStatus(command.Status);
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

        public async Task<IEnumerable<OrderVM>> GetAll() => await _orderRepository.GetAll();

        public async Task<ChangeStatusOrderVM> GetDetailForChangeStatusBy(long id) => await _orderRepository.GetDetailForChangeStatusBy(id);

        public async Task<IEnumerable<OrderItemVM>> GetOrderDetails(long orderId) => await _itemRepository.GetOrderDetails(orderId);

        public async Task<bool> IsThereOpenOrder(long visitorId) => await _orderRepository.IsThereOpenOrder(visitorId);

        public async Task<OperationResult> PlaceOrder(OrderVM command)
        {
            OperationResult result = new();

            if (command.MarketId <= 0) return result.Failed("مارکت را انتخاب کنید");

            var order = await _orderRepository.GetOpenOrder(command.VisitorId);

            if (order is null) return result.Failed(ApplicationMessage.NotExist);
            if (order.VisitorId != command.VisitorId) return result.Failed("این سبد خرید به شما تعلق ندارد");

            foreach (var item in order.OrderItems)
            {
                var product = await _productRepository.GetEntityByIdAsync(item.ProductId);

                if (product is null) return result.Failed("چنین محصولی وجود ندارد");
                if (product.Count <= 0) return result.Failed($"محصول {product.Title} کمتر از تعداد درخواستی در انبار هست");
                if (product.Count - item.Count < 0) return result.Failed($"محصول {product.Title} کمتر از تعداد درخواستی در انبار هست");

                product.ReduceCount(item.Count);
                var discount = await _discountRepository.GetBy(product.Id);

                if (discount is null)
                    item.PlaceOrder(product.PurchacePrice, 0);
                else
                    item.PlaceOrder(product.PurchacePrice, (discount.DiscountRate * product.PurchacePrice) / 100);
            }

            order.PlaceOrder(command.MarketId, command.TotalPrice, command.TotalDiscount, command.PayAmount);
            await _orderRepository.SaveChangesAsync();

            return result.Succeeded($"سفارش شما با موفقیت ثبت گردید. کد سفارش شما {order.RefId} می باشد.");
        }

        public async Task<OperationResult> UpdateCountOfItems(long[] itemsId, int[] quantity)
        {
            OperationResult result = new();

            for (int i = 0; i < itemsId.Length; i++)
            {
                var item = await _itemRepository.GetEntityByIdAsync(itemsId[i]);
                if (item is null) return result.Failed(ApplicationMessage.GoesWrong);

                item.ChangeCount(quantity[i]);
            }

            await _itemRepository.SaveChangesAsync();

            return result.Succeeded();
        }
    }
}