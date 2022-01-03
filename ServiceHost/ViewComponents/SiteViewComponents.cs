using Marketer.Query.Queries.Settings;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceHost.ViewComponents
{
    public class SiteHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }

    public class SiteFooterViewComponent : ViewComponent
    {
        private readonly ISettingQuery _settingQuery;

        public SiteFooterViewComponent(ISettingQuery settingQuery) => _settingQuery = settingQuery;

        public async Task<IViewComponentResult>  InvokeAsync() => View(await _settingQuery.Get());
    }

    public class SiteResponsiveMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
