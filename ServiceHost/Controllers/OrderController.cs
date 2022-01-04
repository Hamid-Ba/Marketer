using Framework.Application.Authentication;
using Marketer.Application.Contract.AI.Orders;
using Marketer.Application.Contract.AI.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderApplication _orderApplication;
        private readonly IProductApplication _productApplication;

        public OrderController(IOrderApplication orderApplication, IProductApplication productApplication)
        {
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
                return RedirectToAction("Detail", "Product", new { slug });
            }

            var result = await _orderApplication.AddProductToOpenOrder(User.GetVisitorId(), slug);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
            else TempData[ErrorMessage] = result.Message;

            return View();
        }
    }
}
