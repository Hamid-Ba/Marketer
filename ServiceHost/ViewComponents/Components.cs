using Marketer.Query.Queries.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceHost.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public ProductViewComponent(IProductQuery productQuery) => _productQuery = productQuery;

        public async Task<IViewComponentResult> InvokeAsync(int take) => View(await _productQuery.GetAll(take));
    }
}
