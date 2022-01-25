using Marketer.Application.Contract.AI.Extera;
using Marketer.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Tools;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    [PermissionChecker(MarketerPermissions.SettingManagement)]
    public class ContactUsController : AdminBaseController
    {
        private readonly IContactUsApplication _contactUsApplication;

        public ContactUsController(IContactUsApplication contactUsApplication) => _contactUsApplication = contactUsApplication;

        public async Task<IActionResult> Index() => View(await _contactUsApplication.GetAll());

        [HttpGet]
        public async Task<IActionResult> Detail(long id) => PartialView("Detail", await _contactUsApplication.GetMessageBy(id));
    }
}