using Framework.Application.Authentication;
using Marketer.Application.Contract.AI.Orders;
using Marketer.Application.Contract.AI.Products;
using Marketer.Query.Queries.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderQuery _orderQuery;
        private readonly IOrderApplication _orderApplication;
        private readonly IProductApplication _productApplication;

        public OrderController(IOrderQuery orderQuery, IOrderApplication orderApplication, IProductApplication productApplication)
        {
            _orderQuery = orderQuery;
            _orderApplication = orderApplication;
            _productApplication = productApplication;
        }

        [HttpGet("Basket/{slug}")]
        public async Task<IActionResult> Basket(string slug)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData[WarningMessage] = "برای ثبت سفارش باید ابتدا وارد حساب خود شوید";
                return RedirectToAction("Index", "Home");
            }

            var isInStock = await _productApplication.IsProductExistInStock(slug);

            if (!isInStock.IsSucceeded)
            {
                TempData[WarningMessage] = isInStock.Message;
                return RedirectToAction("Index", "Home");
            }

            var result = await _orderApplication.AddProductToOpenOrder(User.GetVisitorId(), slug);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
            else TempData[ErrorMessage] = result.Message;

            var items = await _orderQuery.GetOpenOrder(User.GetVisitorId());

            return View(items);
        }

        [HttpGet("Basket")]
        public async Task<IActionResult> GetBasket()
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData[WarningMessage] = "ابتدا وارد حساب خود شوید";
                return RedirectToAction("Index", "Home");
            }

            var items = await _orderQuery.GetOpenOrder(User.GetVisitorId());

            if(items is null)
            {
                TempData[WarningMessage] = "سبد خرید شما خالی است";
                return RedirectToAction("Index", "Home");
            }
            return View("Basket", items);
        }
    }
}
