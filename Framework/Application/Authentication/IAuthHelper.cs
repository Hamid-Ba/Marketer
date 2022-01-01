using System.Threading.Tasks;

namespace Framework.Application.Authentication
{
    public interface IAuthHelper
    {
        void SignOut();
        Task SignInAsync(VisitorAuthViewModel account);
    }
}