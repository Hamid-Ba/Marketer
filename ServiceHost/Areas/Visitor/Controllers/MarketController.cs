﻿using Framework.Application.Authentication;
using Marketer.Application.Contract.AI.Products;
using Marketer.Application.Contract.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Visitor.Controllers
{
    public class MarketController : VisitorBaseController
    {
        private readonly IMarketApplication _marketApplication;

        public MarketController(IMarketApplication marketApplication) => _marketApplication = marketApplication;

        public async Task<IActionResult> Index() => View(await _marketApplication.GetAll());

        [HttpGet]
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(CreateMarketVM command)
        {
            command.VisitorId = User.GetVisitorId();
            var result = await _marketApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var result = await _marketApplication.DoesMarketBelongToVisitor(id, User.GetVisitorId());

            if (!result.IsSucceeded)
            {
                TempData[ErrorMessage] = result.Message;
                return RedirectToAction("Index");
            }

            return PartialView(await _marketApplication.GetDetailForEditBy(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditMarketVM command)
        {
            var checkVisitor = await _marketApplication.DoesMarketBelongToVisitor(command.Id, command.VisitorId);

            if (!checkVisitor.IsSucceeded)
            {
                TempData[ErrorMessage] = checkVisitor.Message;
                return RedirectToAction("Index");
            }

            var result = await _marketApplication.Edit(command);

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
            var result = await _marketApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

    }
}