using System.Threading.Tasks;
using Marketer.Application.Contract.AI.Products;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Tools;

namespace ServiceHost.Areas.Admin.Controllers
{
    [PermissionChecker(MarketerPermissions.ProductTypeManagement)]
    public class PackageTypeController : AdminBaseController
    {
        private readonly IPackageTypeApplication _packageTypeApplication;

        public PackageTypeController(IPackageTypeApplication packageTypeApplication) => _packageTypeApplication = packageTypeApplication;

        public async Task<IActionResult> Index() => View(await _packageTypeApplication.GetAll());

        [HttpGet]
        [PermissionChecker(MarketerPermissions.CreateProductType)]
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(CreatePackageTypeVM command)
        {
            var result = await _packageTypeApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        [PermissionChecker(MarketerPermissions.EditProductType)]
        public async Task<IActionResult> Edit(long id) => PartialView(await _packageTypeApplication.GetDetailForEditBy(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditPackageTypeVM command)
        {
            var result = await _packageTypeApplication.Edit(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        [PermissionChecker(MarketerPermissions.DeleteProductType)]
        public IActionResult Delete(long id) => PartialView(id);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeletePackageTypeVM command)
        {
            var result = await _packageTypeApplication.Delete(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }
    }
}