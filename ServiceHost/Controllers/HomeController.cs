using Marketer.Application.Contract.AI.Extera;
using Marketer.Application.Contract.ViewModels.Extera;
using Marketer.Query.Queries.Settings;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISettingQuery _settingQuery;
        private readonly IContactUsApplication _contactUsApplication;

        public HomeController(ISettingQuery settingQuery, IContactUsApplication contactUsApplication)
        {
            _settingQuery = settingQuery;
            _contactUsApplication = contactUsApplication;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Text = await _settingQuery.GetSummaryText();
            return View();
        }

        [Route("about-us")]
        public async Task<IActionResult> AboutUs() => View("AboutUs", await _settingQuery.GetAboutUsTextAsync());

        [HttpGet("contact-us")]
        public async Task<IActionResult> ContactUs()
        {
            ViewBag.Settings = await _settingQuery.Get();
            return View();
        }

        [HttpPost("contact-us")]
        public async Task<IActionResult> ContactUs(CreateContactUs command)
        {
            ViewBag.Settings = await _settingQuery.Get();

            if (ModelState.IsValid)
            {
                var result = await _contactUsApplication.Send(command);

                if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
                else TempData[ErrorMessage] = result.Message;
            }
            return View(command);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("NotFound")]
        public IActionResult NotFound() => View();
    }
}
