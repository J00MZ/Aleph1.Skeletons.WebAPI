using Aleph1.Logging;
using System.Web.Http;
using WebApiThrottle;

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
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}", defaults: new { id = RouteParameter.Optional });

            //Apply Throttling Policy on all Controllers
            //see more configs here: https://github.com/stefanprodan/WebApiThrottle
            config.MessageHandlers.Add(new ThrottlingHandler()
            {
                Policy = new ThrottlePolicy(perSecond: 1, perMinute: 45, perHour: 2_025, perDay: 18_225, perWeek: 95_682)
                {
                    IpThrottling = true,
                    EndpointThrottling = true,
                    StackBlockedRequests = true
                },
                Repository = new CacheRepository()
            });
        }
    }
}
