using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ApiV2.Startup))]
namespace ApiV2
{
    public class Startup
    {
        public virtual void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            ConfigureWebApi(config);
            app.UseWebApi(config);
        }

        public static void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
        }
    }
}