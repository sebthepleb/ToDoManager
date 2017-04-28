using System.Web.Http;

namespace ToDoManager
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("ToDo",
                "api/v1/{controller}/{id}",
                new {controller = "ToDo", id = RouteParameter.Optional}
            );
        }
    }
}