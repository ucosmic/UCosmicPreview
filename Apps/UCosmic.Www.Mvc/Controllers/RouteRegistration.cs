using System;
using System.Linq;
using System.Reflection;
using System.Web.Routing;

namespace UCosmic.Www.Mvc.Controllers
{
    public static class RouteRegistration
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var assemblies = new[] { Assembly.GetExecutingAssembly() };
            foreach (var assembly in assemblies)
            {
                var routeClasses = assembly.GetTypes()
                    .Where(t => t != typeof(Route) && typeof(Route).IsAssignableFrom(t))
                    .ToArray()
                ;
                foreach (var routeClass in routeClasses)
                {
                    routes.Add((Route)Activator.CreateInstance(routeClass));
                }
            }
        }
    }
}