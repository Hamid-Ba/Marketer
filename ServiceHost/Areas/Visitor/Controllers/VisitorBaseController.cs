using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Visitor.Controllers
{
    [Area("Visitor")]
    public class VisitorBaseController : Controller
    {
        protected string ErrorMessage = "ErrorMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string InfoMessage = "InfoMessage";
        protected string WarningMessage = "WarningMessage";
    }
}
