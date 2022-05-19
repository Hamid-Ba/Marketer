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
        public IActionResult VisitorLogin() => User.Identity != null && User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VisitorLogin(LoginVisitorVM command)
        {
            if (ModelState.IsValid)
            {
                var result = await _visitorApplication.Login(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index", "Home",new {Areas = ""});
                }
                TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }

        #endregion

        #region Operator Login

        [HttpGet]
        [Route("AdminLogin")]
        public IActionResult OperatorLogin() => User.Identity != null && User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : View();

        [HttpPost]
        [Route("AdminLogin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OperatorLogin(LoginOperatorVM command)
        {
            if (ModelState.IsValid)
            {
                var result = await _operatorApplication.Login(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index", "Home",new {Areas = ""});
                }

                TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }

        #endregion

        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                _authHelper.SignOut();
                TempData[SuccessMessage] = "با موفقیت خارج شدید";
            }
            else
                TempData[ErrorMessage] = "هنوز وارد نشده اید که";


            return RedirectToAction("Index", "Home");
        }
    }
}