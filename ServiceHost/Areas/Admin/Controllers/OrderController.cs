using Marketer.Application.Contract.AI.Orders;
using Marketer.Application.Contract.AI.Products;
using Marketer.Application.Contract.ViewModels.Orders;
using Marketer.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Tools;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    [PermissionChecker(MarketerPermissions.OrderManagement)]
    public class OrderController : AdminBaseController
    {
        private readonly IOrderApplication _orderApplication;
        private readonly IMarketApplication _marketApplication;

        public OrderController(IOrderApplication orderApplication, IMarketApplication marketApplication)
        {
            _orderApplication = orderApplication;
            _marketApplication = marketApplication;
        }

        public async Task<IActionResult> Index() => View(await _orderApplication.GetAll());

        [HttpGet]
        public async Task<IActionResult> ChangeStatus(long id) => PartialView(await _orderApplication.GetDetailForChangeStatusBy(id));

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(ChangeStatusOrderVM command)
        {
            var result = await _orderApplication.ChangeStatus(command);
            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetItems(long id) => PartialView(await _orderApplication.GetOrderDetails(id));

        [HttpGet]
        public async Task<IActionResult> Market(long marketId) => PartialView(await _marketApplication.GetBy(marketId));
    }
}