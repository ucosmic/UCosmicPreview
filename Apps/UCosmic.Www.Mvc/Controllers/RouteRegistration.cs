using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace UCosmic.Www.Mvc.Controllers
{
    public static class RouteRegistration
    {
        public static void RegisterRoutes(RouteCollection routes, params Assembly[] assemblies)
        {
            assemblies = assemblies.Length == 0
                ? AppDomain.CurrentDomain.GetAssemblies() : assemblies;
            var routesDictionary = new Dictionary<int, ICollection<Route>>();
            foreach (var routeClass in assemblies.GetRouteClasses())
                routesDictionary.Add(routeClass.Construct());

            foreach (var key in routesDictionary.Keys.OrderBy(k => k))
                foreach (var route in routesDictionary[key])
                    routes.Add(route);
        }

        public static IRouteHandler CreateRouteHandler(bool stopRoutingHandler = false)
        {
            if (!stopRoutingHandler) return new MvcRouteHandler();
            return new StopRoutingHandler();
        }

        public static RouteValueDictionary CreateDataTokens(string area, Type areaRegistrationType)
        {
            var namespaces = new[] { string.Format("{0}.*", areaRegistrationType.Namespace) };
            return new RouteValueDictionary(new
            {
                Namespaces = namespaces,
                area,
                UseNamespaceFallback = true,
            });
        }

        public static RouteValueDictionary CreateDataTokens(string area, string[] namespaces)
        {
            return new RouteValueDictionary(new
            {
                Namespaces = namespaces,
                area,
                UseNamespaceFallback = false,
            });
        }

        private static IEnumerable<Type> GetRouteClasses(this IEnumerable<Assembly> assemblies)
        {
            var routeClasses = new List<Type>();
            foreach (var assembly in assemblies)
            {
                try
                {
                    routeClasses.AddRange(assembly.GetTypes().Where(t =>
                        !t.IsAbstract &&
                        t.GetConstructors().Any(c => !c.GetParameters().Any()) &&
                        typeof(Route).IsAssignableFrom(t)
                    ));
                }
                catch (ReflectionTypeLoadException)
                {
                    //var loaderExceptions = ex.LoaderExceptions;
                }
            }
            return routeClasses.ToArray();
        }

        private static Route Construct(this Type routeClass)
        {
            return (Route)Activator.CreateInstance(routeClass);
        }

        private static void Add(this IDictionary<int, ICollection<Route>> dictionary, Route route)
        {
            var key = 0;
            var mvcRoute = route as MvcRoute;
            if (mvcRoute != null) key = mvcRoute.Order;
            var routesValue = dictionary.ContainsKey(key)
                ? dictionary[key] : dictionary[key] = new List<Route>();
            routesValue.Add(route);
            if (mvcRoute != null && mvcRoute.AlternateUrls != null && mvcRoute.AlternateUrls.Any())
                foreach (var alternateUrl in mvcRoute.AlternateUrls)
                    routesValue.Add(new Route(alternateUrl, new MvcRouteHandler())
                    {
                        DataTokens = mvcRoute.DataTokens,
                        Defaults = mvcRoute.Defaults,
                        Constraints = mvcRoute.Constraints,
                    });
        }
    }
}