using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace TravelAlberta.Exercise
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();
            Server.ClearError();

            Response.TrySkipIisCustomErrors = true;
            Response.RedirectToRoute("Error", new { error = new HandleErrorInfo(exception, "Error", "Index") });
        }
    }
}
