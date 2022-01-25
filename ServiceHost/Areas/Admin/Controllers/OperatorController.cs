using Marketer.Application.Contract.AI.Account;
using Marketer.Application.Contract.ViewModels.Account;
using Marketer.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Tools;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    [PermissionChecker(MarketerPermissions.OperatorManagement)]
    public class OperatorController : AdminBaseController
    {
        private readonly IRoleApplication _roleApplication;
        private readonly IOperatorApplication _operatorApplication;

        public OperatorController(IRoleApplication roleApplication, IOperatorApplication operatorApplication)
        {
            _roleApplication = roleApplication;
            _operatorApplication = operatorApplication;
        }

        public async Task<IActionResult> Index() => View(await _operatorApplication.GetAll());

        [HttpGet]
        [PermissionChecker(MarketerPermissions.CreateOperator)]
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = new SelectList(await _roleApplication.GetAll(), "Id", "Name");
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOperatorVM command)
        {
            if (!ModelState.IsValid) ViewBag.Roles = new SelectList(await _roleApplication.GetAll(), "Id", "Name");

            var result = await _operatorApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        [PermissionChecker(MarketerPermissions.EditOperator)]
        public async Task<IActionResult> Edit(long id)
        {
            ViewBag.Roles = new SelectList(await _roleApplication.GetAll(), "Id", "Name");
            return PartialView(await _operatorApplication.GetDetailForEditBy(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditOperatorVM command)
        {
            if (!ModelState.IsValid) ViewBag.Roles = new SelectList(await _roleApplication.GetAll(), "Id", "Name");

            var result = await _operatorApplication.Edit(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        [PermissionChecker(MarketerPermissions.DeleteOperator)]
        public IActionResult Delete(long id) => PartialView(id);

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(long id)
        {
            var result = await _operatorApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

    }
}
