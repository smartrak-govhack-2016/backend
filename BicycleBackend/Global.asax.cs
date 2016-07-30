using System.Web.Http;

namespace BicycleBackend
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Cache.DoNothing();
        }
    }
}
