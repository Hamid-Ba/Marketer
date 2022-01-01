using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class SiteHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }

    public class SiteFooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }

    public class SiteResponsiveMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
