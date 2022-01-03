using Framework.Application;
using Marketer.Query.Queries.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ReflectionIT.Mvc.Paging;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductQuery _productQuery;

        public ProductsController(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public async Task<IActionResult> Index(ProductSort sortExp, string search, int pageIndex = 1)
        {
            var products = await _productQuery.GetAll(sortExp,search);

            var model = PagingList.Create(products, 6, pageIndex);

            model.RouteValue = new RouteValueDictionary
            {
                { "sortExp", sortExp },
                { "search",  search }
            };

            return View(model);
        }
    }
}