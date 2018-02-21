using Aleph1.Logging;
using System.Web.Http;

namespace Aleph1.Skeletons.WebAPI.WebAPI
{
    /// <summary>web api congigurations</summary>
    internal static class WebApiConfig
    {
        /// <summary>Registers web api configurations</summary>
        /// <param name="config">The current configuration</param>
        [Logged(LogParameters = false)]
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
