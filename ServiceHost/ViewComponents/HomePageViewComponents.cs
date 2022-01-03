using Marketer.Query.Queries.Brands;
using Marketer.Query.Queries.Categories;
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

    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryQuery _categoryQuery;

        public CategoryViewComponent(ICategoryQuery categoryQuery) => _categoryQuery = categoryQuery;

        public async Task<IViewComponentResult> InvokeAsync() => View(await _categoryQuery.GetAll());

    }

    public class ProductsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}