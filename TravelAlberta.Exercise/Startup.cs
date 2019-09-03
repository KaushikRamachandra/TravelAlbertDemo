using Microsoft.Owin;
using Owin;
using TravelAlberta.Exercise.Services;

[assembly: OwinStartup(typeof(TravelAlberta.Exercise.Startup))]
namespace TravelAlberta.Exercise
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ApplicationConfigService appConfiguration = new ApplicationConfigService();
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseAutofacMiddleware(Bootstrapper.Run(appConfiguration));
            app.UseAutofacMvc();
        }
    }
}
