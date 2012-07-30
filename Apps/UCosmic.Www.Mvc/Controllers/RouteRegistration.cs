using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace UCosmic.Www.Mvc.Controllers
{
    public static class RootActionRouter
    {
        public static void RegisterRoutes(Type routeMapper, AreaRegistrationContext context, string area, string controller)
        {
            var nestedMappers = routeMapper.GetNestedTypes(BindingFlags.Public | BindingFlags.Static);
            foreach (var mapper in nestedMappers)
            {
                var method = mapper.GetMethod("MapRoutes", BindingFlags.Public | BindingFlags.Static);
                if (method == null) continue;
                mapper.InvokeMember("MapRoutes", BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod,
                    null, routeMapper, new object[] { context, area, controller });
            }
        }
    }

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