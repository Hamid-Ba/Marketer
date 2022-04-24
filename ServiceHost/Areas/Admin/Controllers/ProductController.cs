using Marketer.Application.Contract.AI.Products;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Tools;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    [PermissionChecker(MarketerPermissions.ProductManagement)]
    public class ProductController : AdminBaseController
    {
        private readonly IBrandApplication _brandApplication;
        private readonly IProductApplication _productApplication;
        private readonly ICategoryApplication _categoryApplication;
        private readonly IPackageTypeApplication _packageTypeApplication;

        public ProductController(IBrandApplication brandApplication, IProductApplication productApplication,
            ICategoryApplication categoryApplication, IPackageTypeApplication packageTypeApplication)
        {
            _brandApplication = brandApplication;
            _productApplication = productApplication;
            _categoryApplication = categoryApplication;
            _packageTypeApplication = packageTypeApplication;
        }

        public async Task<IActionResult> Index() => View(await _productApplication.GetAll());

        [HttpGet]
        [PermissionChecker(MarketerPermissions.CreateProduct)]
        public async Task<IActionResult> Create()
        {
            ViewBag.Brands = new SelectList(await _brandApplication.GetAllForSelection(), "Id", "Name");
            ViewBag.Categories = new SelectList(await _categoryApplication.GetAllForSelection(), "Id", "Name");
            ViewBag.Packages = new SelectList(await _packageTypeApplication.GetAll(), "Id", "Title");

            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM command)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = new SelectList(await _brandApplication.GetAllForSelection(), "Id", "Name");
                ViewBag.Categories = new SelectList(await _categoryApplication.GetAllForSelection(), "Id", "Name");
                ViewBag.Packages = new SelectList(await _packageTypeApplication.GetAll(), "Id", "Title");
            }

            var result = await _productApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        [PermissionChecker(MarketerPermissions.EditProduct)]
        public async Task<IActionResult> Edit(long id)
        {
            ViewBag.Brands = new SelectList(await _brandApplication.GetAllForSelection(), "Id", "Name");
            ViewBag.Categories = new SelectList(await _categoryApplication.GetAllForSelection(), "Id", "Name");
            ViewBag.Packages = new SelectList(await _packageTypeApplication.GetAll(), "Id", "Title");

            return PartialView(await _productApplication.GetDetailForEditBy(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductVM command)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = new SelectList(await _brandApplication.GetAllForSelection(), "Id", "Name");
                ViewBag.Categories = new SelectList(await _categoryApplication.GetAllForSelection(), "Id", "Name");
                ViewBag.Packages = new SelectList(await _packageTypeApplication.GetAll(), "Id", "Title");
            }

            var result = await _productApplication.Edit(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        [PermissionChecker(MarketerPermissions.DeleteProduct)]
        public IActionResult Delete(long id) => PartialView(id);

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(long id)
        {
            var result = await _productApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

    }
}