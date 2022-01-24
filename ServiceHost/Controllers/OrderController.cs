using Framework.Application.Authentication;
using Marketer.Application.Contract.AI.Orders;
using Marketer.Application.Contract.AI.Products;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Query.Queries.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderQuery _orderQuery;
        private readonly IConfiguration _configuration;
        private readonly IOrderApplication _orderApplication;
        private readonly IProductApplication _productApplication;

        public OrderController(IOrderQuery orderQuery, IConfiguration configuration, IOrderApplication orderApplication, IProductApplication productApplication)
        {
            _orderQuery = orderQuery;
            _configuration = configuration;
            _orderApplication = orderApplication;
            _productApplication = productApplication;
        }

        [HttpGet("Basket/{slug}")]
        public async Task<IActionResult> Basket(string slug)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value != "Visitor")
            {
                TempData[WarningMessage] = "فقط بازاریاب می تواند سفارش ایجاد کند";
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

            ViewBag.Url = _configuration.GetSection("Url").Value;
            return View(items);
        }

        [HttpGet("Basket")]
        public async Task<IActionResult> GetBasket()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value != "Visitor")
            {
                TempData[WarningMessage] = "فقط بازاریاب می تواند سفارش ایجاد کند";
                return RedirectToAction("Index", "Home");
            }

            var items = await _orderQuery.GetOpenOrder(User.GetVisitorId());

            if (items is null || items.Items.Count == 0)
            {
                TempData[WarningMessage] = "سبد خرید شما خالی است";
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Url = _configuration.GetSection("Url").Value;
            return View("Basket", items);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(long id, long productId)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value != "Visitor")
            {
                TempData[WarningMessage] = "فقط بازاریاب می تواند سفارش ایجاد کند";
                return RedirectToAction("Index", "Home");
            }

            var result = await _orderApplication.DeleteOrderItemBy(User.GetVisitorId(), id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
            else
            {
                TempData[ErrorMessage] = result.Message;
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("GetBasket");
        }

        [HttpPost]
        public StatusCheckVM CheckStock([FromBody] CheckCartItemCountVM command) => _productApplication.CheckStock(command);

        [HttpPost]
        public async Task<IActionResult> UpdateBasket(long[] itemsId, int[] quantity, long[] productsId)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value != "Visitor")
            {
                TempData[WarningMessage] = "فقط بازاریاب می تواند سفارش ایجاد کند";
                return RedirectToAction("Index", "Home");
            }

            var isStockOk = await _productApplication.IsCountSatisfyStock(productsId, quantity);

            if (isStockOk.IsSucceeded)
            {
                var updateItemStock = await _orderApplication.UpdateCountOfItems(itemsId, quantity);

                if (updateItemStock.IsSucceeded)
                {
                    TempData[SuccessMessage] = updateItemStock.Message;
                    return RedirectToAction("FinalBasket");
                }

                TempData[ErrorMessage] = updateItemStock.Message;
            }

            else TempData[ErrorMessage] = isStockOk.Message;

            return RedirectToAction("GetBasket");
        }

        #region Checkout

        [HttpGet]
        public async Task<IActionResult> FinalBasket()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value != "Visitor")
            {
                TempData[WarningMessage] = "فقط بازاریاب می تواند سفارش ایجاد کند";
                return RedirectToAction("Index", "Home");
            }

            return View(await _orderQuery.CalculateOrder(User.GetVisitorId()));
        }

        #endregion
    }
}