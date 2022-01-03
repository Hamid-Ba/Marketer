using Marketer.Query.Queries.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductQuery _productQuery;

        public ProductController(IProductQuery productQuery) => _productQuery = productQuery;

        [Route("product/{slug}")]
        public async Task<IActionResult> Detail(string slug) => View(await _productQuery.GetBy(slug));
        
    }
}