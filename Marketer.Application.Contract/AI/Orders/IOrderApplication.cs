﻿using Framework.Application;
using Marketer.Application.Contract.ViewModels.Orders;
using System.Threading.Tasks;

namespace Marketer.Application.Contract.AI.Orders
{
    public interface IOrderApplication
    {
        Task<bool> IsThereOpenOrder(long visitorId);
        Task<int> CountOfProductInItem(long orderItemId);
        Task<OperationResult> PlaceOrder(OrderVM command);
        Task<OperationResult> CreateOrder(long visitorId);
        Task<OperationResult> UpdateCountOfItems(long[] itemsId, int[] quantity);
        Task<OperationResult> DeleteOrderItemBy(long visitorId,long orderItemId);
        Task<OperationResult> AddProductToOpenOrder(long visitorId,string productSlug);
    }
}
