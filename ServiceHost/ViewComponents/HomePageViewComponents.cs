using Marketer.Query.Queries.Brands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceHost.ViewComponents
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly IBrandQuery _brandQuery;

        public BrandViewComponent(IBrandQuery brandQuery) => _brandQuery = brandQuery;

        public async Task<IViewComponentResult> InvokeAsync() => View(await _brandQuery.GetAll());

    }
}