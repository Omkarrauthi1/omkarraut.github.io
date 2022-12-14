using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace bookworm
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.MapHttpAttributeRoutes();
            config.EnableCors();
           

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{param}",
                defaults: new { param = RouteParameter.Optional }
            );
        }
    }
}
