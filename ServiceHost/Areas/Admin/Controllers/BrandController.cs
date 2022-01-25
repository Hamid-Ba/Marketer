using Marketer.Application.Contract.AI.Products;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Tools;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    [PermissionChecker(MarketerPermissions.BrandManagement)]
    public class BrandController : AdminBaseController
    {
        private readonly IBrandApplication _brandApplication;

        public BrandController(IBrandApplication categoryApplication) => _brandApplication = categoryApplication;

        public async Task<IActionResult> Index() => View(await _brandApplication.GetAll());

        [HttpGet]
        [PermissionChecker(MarketerPermissions.CreateBrand)]
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(CreateBrandVM command)
        {
            var result = await _brandApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        [PermissionChecker(MarketerPermissions.EditBrand)]
        public async Task<IActionResult> Edit(long id) => PartialView(await _brandApplication.GetDetailForEditBy(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBrandVM command)
        {
            var result = await _brandApplication.Edit(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        [PermissionChecker(MarketerPermissions.DeleteBrand)]
        public IActionResult Delete(long id) => PartialView(id);

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(long id)
        {
            var result = await _brandApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }
    }
}
