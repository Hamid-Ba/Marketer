using Marketer.Application.Contract.AI.Products;
using Marketer.Application.Contract.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController
    {
        private readonly ICategoryApplication _categoryApplication;

        public CategoryController(ICategoryApplication categoryApplication) => _categoryApplication = categoryApplication;

        public async Task<IActionResult> Index() => View(await _categoryApplication.GetAll());

        [HttpGet]
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM command)
        {
            var result = await _categoryApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id) => PartialView(await _categoryApplication.GetDetailForEditBy(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCategoryVM command)
        {
            var result = await _categoryApplication.Edit(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult Delete(long id) => PartialView(id);

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(long id)
        {
            var result = await _categoryApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

    }
}
