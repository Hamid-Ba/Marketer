using Marketer.Application.Contract.AI.Discounts;
using Marketer.Application.Contract.AI.Products;
using Marketer.Application.Contract.ViewModels.Discounts;
using Marketer.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Tools;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    [PermissionChecker(MarketerPermissions.DiscountManagement)]
    public class DiscountController : AdminBaseController
    {
        private readonly IProductApplication _productApplication;
        private readonly IDiscountApplication _discountApplication;

        public DiscountController(IProductApplication productApplication, IDiscountApplication discountApplication)
        {
            _productApplication = productApplication;
            _discountApplication = discountApplication;
        }

        public async Task<IActionResult> Index() => View(await _discountApplication.GetAll());

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Products = new SelectList(await _productApplication.GetAllForSelection(), "Id", "Title");
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDiscountVM command)
        {
            if (!ModelState.IsValid) ViewBag.Products = new SelectList(await _productApplication.GetAllForSelection(), "Id", "Title");

            var result = await _discountApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            ViewBag.Products = new SelectList(await _productApplication.GetAllForSelection(), "Id", "Title");
            return PartialView(await _discountApplication.GetDetailForEditBy(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditDiscountVM command)
        {
            if (!ModelState.IsValid) ViewBag.Products = new SelectList(await _productApplication.GetAllForSelection(), "Id", "Title");

            var result = await _discountApplication.Edit(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult Delete(long id) => PartialView(id);

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(long id)
        {
            var result = await _discountApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

    }
}
