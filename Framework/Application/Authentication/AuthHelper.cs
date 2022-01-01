using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Framework.Application.Authentication
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _httpContext;

        public AuthHelper(IHttpContextAccessor httpContext) => _httpContext = httpContext;

        public async Task SignInAsync(VisitorAuthViewModel account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Role, "Visitor"),
                new Claim(ClaimTypes.Name, account.Fullname),
                new Claim(ClaimTypes.MobilePhone, account.Mobile),
                new Claim("Code", account.Code),

            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties();


            await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        public async Task SignInAsync(OperatorAuthViewModel account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Role, "Operator"),
                new Claim("RoleId", account.RoleId.ToString()),
                new Claim(ClaimTypes.Name, account.Fullname),
                new Claim(ClaimTypes.MobilePhone, account.Mobile),

            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties();


            await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);
        }
        public void SignOut() => _httpContext.HttpContext.SignOutAsync();
    }
}