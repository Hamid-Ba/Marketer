using Framework.Application.Authentication;
using Marketer.Application.Contract.AI.Account;
using Marketer.Application.Contract.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAuthHelper _authHelper;
        private readonly IVisitorApplication _visitorApplication;
        private readonly IOperatorApplication _operatorApplication;

        public AccountController(IAuthHelper authHelper, IVisitorApplication visitorApplication, IOperatorApplication operatorApplication)
        {
            _authHelper = authHelper;
            _visitorApplication = visitorApplication;
            _operatorApplication = operatorApplication;
        }


        #region Visitor Login

        [HttpGet]
        public IActionResult VisitorLogin() => User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VisitorLogin(LoginVisitorVM command)
        {
            if (ModelState.IsValid && !User.Identity.IsAuthenticated)
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

        #endregion

        #region Operator Login

        [HttpGet("adminlogin")]
        public IActionResult OperatorLogin() => User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : View();

        [HttpPost("adminlogin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OperatorLogin(LoginOperatorVM command)
        {
            if (ModelState.IsValid && !User.Identity.IsAuthenticated)
            {
                var result = await _operatorApplication.Login(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index", "Home");
                }
                else TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }

        #endregion

        public IActionResult Logout()
        {
            if(!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

            _authHelper.SignOut();
            TempData[SuccessMessage] = "شما با موفقیت خارج شدید";
            return RedirectToAction("Index", "Home");
        }
    }
}