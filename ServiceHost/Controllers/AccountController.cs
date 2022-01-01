using Marketer.Application.Contract.AI.Account;
using Marketer.Application.Contract.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IVisitorApplication _visitorApplication;

        public AccountController(IVisitorApplication visitorApplication) => _visitorApplication = visitorApplication;

        [HttpGet]
        public IActionResult VisitorLogin() => User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VisitorLogin(LoginVisitorVM command)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                var result = await _visitorApplication.Login(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index", "Home");
                }
                else TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }

    }
}