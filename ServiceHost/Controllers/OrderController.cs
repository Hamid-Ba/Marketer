using Marketer.Application.Contract.AI.Products;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IProductApplication _productApplication;

        [HttpGet("Basket/{slug}")]
        public IActionResult Basket(string slug)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData[WarningMessage] = "برای ثبت سفارش باید ابتدا وارد حساب خود شوید";
                return RedirectToAction("Index", "Home");
            }

            //var product = await _productApplication.get

            return View();
        }
    }
}
