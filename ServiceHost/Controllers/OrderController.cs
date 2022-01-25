using Framework.Application.Authentication;
using Marketer.Application.Contract.AI.Orders;
using Marketer.Application.Contract.AI.Products;
using Marketer.Application.Contract.ViewModels.Orders;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Query.Queries.Markets;
using Marketer.Query.Queries.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderQuery _orderQuery;
        private readonly IMarketQuery _marketQuery;
        private readonly IConfiguration _configuration;
        private readonly IOrderApplication _orderApplication;
        private readonly IProductApplication _productApplication;

        public OrderController(IOrderQuery orderQuery, IMarketQuery marketQuery,
            IConfiguration configuration, IOrderApplication orderApplication, IProductApplication productApplication)
        {
            _orderQuery = orderQuery;
            _marketQuery = marketQuery;
            _configuration = configuration;
            _orderApplication = orderApplication;
            _productApplication = productApplication;
        }

        #region Basket

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
        public async Task<IActionResult> UpdateBasket(UpdateBasketVM basket)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value != "Visitor")
            {
                TempData[WarningMessage] = "فقط بازاریاب می تواند سفارش ایجاد کند";
                return RedirectToAction("Index", "Home");
            }

            var isStockOk = await _productApplication.IsCountSatisfyStock(basket.ProductsId, basket.Quantity);

            if (isStockOk.IsSucceeded)
            {
                var updateItemStock = await _orderApplication.UpdateCountOfItems(basket.ItemsId, basket.Quantity);

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

        #endregion

        #region Checkout

        [HttpGet]
        public async Task<IActionResult> FinalBasket()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value != "Visitor")
            {
                TempData[WarningMessage] = "فقط بازاریاب می تواند سفارش ایجاد کند";
                return RedirectToAction("Index", "Home");
            }

            var finalBasketVM = await _orderQuery.CalculateOrder(User.GetVisitorId());

            if (finalBasketVM is null)
            {
                TempData[WarningMessage] = "سبد خرید شما خالیست";
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Markets = new SelectList(await _marketQuery.GetAllBy(User.GetVisitorId()), "Id", "MarketWithCity");
            return View(finalBasketVM);
        }

        [HttpPost]
        public async Task<IActionResult> FinalBasket(FinalBasketVM command)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value != "Visitor")
            {
                TempData[WarningMessage] = "فقط بازاریاب می تواند سفارش ایجاد کند";
                return RedirectToAction("Index", "Home");
            }

            var finalBasketVM = await _orderQuery.CalculateOrder(User.GetVisitorId());

            if (finalBasketVM is null)
            {
                TempData[WarningMessage] = "سبد خرید شما خالیست";
                return RedirectToAction("Index", "Home");
            }

            OrderVM order = new()
            {
                Id = finalBasketVM.OrderId,
                MarketId = command.MarketId,
                PayAmount = finalBasketVM.PayAmount,
                TotalDiscount = finalBasketVM.TotalDiscount,
                TotalPrice = finalBasketVM.TotalPrice,
                VisitorId = finalBasketVM.VisitorId
            };

            var result = await _orderApplication.PlaceOrder(order);

            if (result.IsSucceeded)
            {
                TempData[SuccessMessage] = result.Message;
                return RedirectToAction("Index", "Home");
            }

            TempData[ErrorMessage] = result.Message;

            ViewBag.Markets = new SelectList(await _marketQuery.GetAllBy(User.GetVisitorId()), "Id", "MarketWithCity");
            return View(finalBasketVM);
        }

        #endregion
    }
}