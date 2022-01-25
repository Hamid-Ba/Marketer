using Marketer.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Tools;

namespace ServiceHost.Areas.Admin.Controllers
{
    [PermissionChecker(MarketerPermissions.SiteManagement)]
    public class DashboardController : AdminBaseController
    {
        public IActionResult Index() => View();
    }
}
