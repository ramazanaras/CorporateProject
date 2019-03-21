using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CorporateProject.WebApi.MessageHandlers;

namespace CorporateProject.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
          
            //yapılan her isteğin önünde bu metot çalışacak.yani webapiye gelen her istek öncesi bu sınıfa düşeceğiz.
            config.MessageHandlers.Add(new AuthenticationHandler());
          
            
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
