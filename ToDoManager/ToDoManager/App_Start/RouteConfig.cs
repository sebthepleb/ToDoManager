using System.Web.Mvc;
using System.Web.Routing;

namespace ToDoManager
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Manage",
                "Manage/{action}/{id}",
                new {controller = "Manage", action = "List", id = UrlParameter.Optional}
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Home", id = UrlParameter.Optional}
            );
        }
    }
}