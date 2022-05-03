using System.Web.Http;
using System.Web.Http.Cors;

namespace MLS.OldApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var cors = new EnableCorsAttribute("http://localhost:4200", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("MapByAction", "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //This will used the HTTP header: "Authorization" Value: "Bearer 1234123412341234asdfasdfasdfasdf"
            // configuration.Filters.Add(new JwtAuthenticationAttribute());
        }
    }
}
