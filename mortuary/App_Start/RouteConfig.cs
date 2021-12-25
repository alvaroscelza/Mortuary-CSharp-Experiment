using System.Web.Mvc;
using System.Web.Routing;

namespace mortuary
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Spanish",
                url: "{languageCode}/{controller}/{action}/{id}",
                defaults: new { languageCode = "es", controller = "Providers", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
