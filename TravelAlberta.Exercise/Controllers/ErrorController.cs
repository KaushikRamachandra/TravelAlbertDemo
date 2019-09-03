using System.Net;
using System.Web.Mvc;

namespace TravelAlberta.Exercise.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            return View("Error"); // ~/Shared/Error.cshtml
        }

        [HttpGet, Route("Error")]
        public ViewResult Index(HandleErrorInfo error)
        {
            return View("Error", error); // ~/Shared/Error.cshtml
        }

        [HttpGet]
        public ViewResult NotFound()
        {
            Response.StatusCode = (int) HttpStatusCode.NotFound;
            Response.TrySkipIisCustomErrors = true;

            return View();
        }

        [HttpGet]
        public ViewResult NotFound(HandleErrorInfo error)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            Response.TrySkipIisCustomErrors = true;

            return View(error);
        }

        [HttpGet]
        public ViewResult Forbidden()
        {
            Response.StatusCode = (int) HttpStatusCode.Forbidden;
            Response.TrySkipIisCustomErrors = true;

            return View();
        }

        [HttpGet]
        public ViewResult NoPermissions(string userName)
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            Response.TrySkipIisCustomErrors = true;
            ViewBag.UserName = userName;

            return View();
        }

        [HttpGet]
        public ViewResult Forbidden(HandleErrorInfo error)
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            Response.TrySkipIisCustomErrors = true;

            return View(error);
        }

        [HttpGet]
        public ViewResult ServerError(HandleErrorInfo error)
        {
            Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            Response.TrySkipIisCustomErrors = true;

            return View(error);
        }
    }
}