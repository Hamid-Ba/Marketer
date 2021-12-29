using Marketer.Application.Contract.AI.Account;
using Marketer.Application.Contract.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class RoleController : AdminBaseController
    {
        private readonly IRoleApplication _roleApplication;
        private readonly IPermissionApplication _permissionApplication;

        public RoleController(IRoleApplication roleApplication, IPermissionApplication permissionApplication)
        {
            _roleApplication = roleApplication;
            _permissionApplication = permissionApplication;
        }

        public async Task<IActionResult> Index() => View(await _roleApplication.GetAll());

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Permissions = new SelectList(await _permissionApplication.GetAll(), "Id", "Title");
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleVM command)
        {
            if (!ModelState.IsValid) ViewBag.Permissions = new SelectList(await _permissionApplication.GetAll(), "Id", "Title");

            var result = await _roleApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            ViewBag.Permissions = new SelectList(await _permissionApplication.GetAll(), "Id", "Title");
            return PartialView(await _roleApplication.GetDetailForEditBy(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditRoleVM command)
        {
            if (!ModelState.IsValid) ViewBag.Permissions = new SelectList(await _permissionApplication.GetAll(), "Id", "Title");

            var result = await _roleApplication.Edit(command);

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
            var result = await _roleApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

    }
}