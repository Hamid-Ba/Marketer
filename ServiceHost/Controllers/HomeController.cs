using Marketer.Query.Queries.Settings;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISettingQuery _settingQuery;

        public HomeController(ISettingQuery settingQuery) => _settingQuery = settingQuery;

        public async Task<IActionResult>  Index()
        {
            ViewBag.Text = await _settingQuery.GetSummaryText();
            return View();
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
    }
}
