using Marketer.Application.Contract.AI.Extera;
using Marketer.Application.Contract.ViewModels.Extera;
using Marketer.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Tools;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    [PermissionChecker(MarketerPermissions.SettingManagement)]
    public class SettingController : AdminBaseController
    {
        private readonly ISettingApplication _settingApplication;

        public SettingController(ISettingApplication settingApplication) => _settingApplication = settingApplication;

        [HttpGet]
        public async Task<IActionResult> Modify() => View(await _settingApplication.GetSetting());


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(SettingVM command)
        {
            var result = await _settingApplication.Modify(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
            else TempData[ErrorMessage] = result.Message;

            return RedirectToAction("Modify");
        }
    }
}
