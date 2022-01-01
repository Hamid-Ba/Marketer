using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Visitor.Controllers
{
    public class DashboardController : VisitorBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
