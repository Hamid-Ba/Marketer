using Marketer.Application.Contract.AI.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class MarketController : AdminBaseController
    {
        private readonly IMarketApplication _marketApplication;

        public MarketController(IMarketApplication marketApplication) => _marketApplication = marketApplication;

        public async Task<IActionResult> Index() => View(await _marketApplication.GetAll());
        
    }
}