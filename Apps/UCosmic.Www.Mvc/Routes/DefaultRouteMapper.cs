using System;
using System.Reflection;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Routes
{
    public static class DefaultRouteMapper
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
}