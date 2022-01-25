using Framework.Application.Authentication;
using Marketer.Application.Contract.AI.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ServiceHost.Tools
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IOperatorApplication _userApplication;
        private int permissionId;

        public PermissionCheckerAttribute(int permissionId) => this.permissionId = permissionId;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _userApplication = (IOperatorApplication)context.HttpContext.RequestServices.GetService(typeof(IOperatorApplication));

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                long userId = context.HttpContext.User.GetVisitorId();

                if (!_userApplication.IsOperatorHasPermissions(permissionId, userId)) { context.Result = new RedirectResult("/NotFound"); }
            }

            else context.Result = new RedirectResult("/NotFound");
        }
    }
}
