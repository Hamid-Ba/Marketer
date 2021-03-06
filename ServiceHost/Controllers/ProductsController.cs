using Framework.Application;
using Marketer.Query.Queries.Brands;
using Marketer.Query.Queries.Categories;
using Marketer.Query.Queries.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ReflectionIT.Mvc.Paging;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IBrandQuery _brandQuery;
        private readonly IProductQuery _productQuery;
        private readonly ICategoryQuery _categoryQuery;

        public ProductsController(IBrandQuery brandQuery, IProductQuery productQuery, ICategoryQuery categoryQuery)
        {
            _brandQuery = brandQuery;
            _productQuery = productQuery;
            _categoryQuery = categoryQuery;
        }

        [HttpGet("Products")]
        [HttpGet("Products/{catSlug}")]
        public async Task<IActionResult> Index(ProductSort sortExp, string search, string catSlug, int pageIndex = 1)
        {
            var products = await _productQuery.GetAll(sortExp, search, catSlug,null);

            ViewBag.Brands = await _brandQuery.GetAll();
            ViewBag.Categories = await _categoryQuery.GetAll();

            var model = PagingList.Create(products, 6, pageIndex);

            model.RouteValue = new RouteValueDictionary
            {
                { "sortExp", sortExp },
                { "search",  search } ,
                { "catSlug",  catSlug }
            };

            return View(model);
        }

        [HttpGet("Brand/{brandSlug}")]
        public async Task<IActionResult> Brand(ProductSort sortExp, string search,string brandSlug, string catSlug, int pageIndex = 1)
        {
            var products = await _productQuery.GetAll(sortExp, search, catSlug, brandSlug);

            ViewBag.Brands = await _brandQuery.GetAll();
            ViewBag.Categories = await _categoryQuery.GetAll();

            var model = PagingList.Create(products, 6, pageIndex);

            model.RouteValue = new RouteValueDictionary
            {
                { "sortExp", sortExp },
                { "search",  search } ,
                { "catSlug",  catSlug }
            };

            return View("Index",model);
        }
    }
}