using Marketer.Application.Contract.AI.Extera;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class ContactUsController : AdminBaseController
    {
        private readonly IContactUsApplication _contactUsApplication;

        public ContactUsController(IContactUsApplication contactUsApplication) => _contactUsApplication = contactUsApplication;

        public async Task<IActionResult> Index() => View(await _contactUsApplication.GetAll());

        [HttpGet]
        public async Task<IActionResult> Detail(long id) => PartialView("Detail", await _contactUsApplication.GetMessageBy(id));
    }
}