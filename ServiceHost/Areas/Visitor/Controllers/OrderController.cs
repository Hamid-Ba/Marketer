using Framework.Application.Authentication;
using Marketer.Application.Contract.AI.Orders;
using Marketer.Application.Contract.AI.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Visitor.Controllers
{
    public class OrderController : VisitorBaseController
    {
        private readonly IOrderApplication _orderApplication;
        private readonly IMarketApplication _marketApplication;

        public OrderController(IOrderApplication orderApplication, IMarketApplication marketApplication)
        {
            _orderApplication = orderApplication;
            _marketApplication = marketApplication;
        }

        public async Task<IActionResult> Index() => View(await _orderApplication.GetAllBy(User.GetVisitorId()));

        [HttpGet]
        public async Task<IActionResult> GetItems(long id) => PartialView(await _orderApplication.GetOrderDetailsBy(id,User.GetVisitorId()));

        [HttpGet]
        public async Task<IActionResult> Market(long marketId)
        {
            var result = await _marketApplication.DoesMarketBelongToVisitor(marketId,User.GetVisitorId());

            if(!result.IsSucceeded)
            {
                TempData[ErrorMessage] = result.Message;
                return RedirectToAction("Index");
            }

           return PartialView(await _marketApplication.GetBy(marketId));
        }
    }
}
