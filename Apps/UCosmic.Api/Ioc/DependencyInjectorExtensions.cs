using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic
{
    public static class DependencyInjectorExtensions
    {
        public static TService GetService<TService>(this IServiceProvider injector)
        {
            return (TService)injector.GetService(typeof(TService));
        }

        public static IEnumerable<TService> GetServices<TService>(this IServiceProvider injector)
        {
            var genericEnumerable = typeof(IEnumerable<>).MakeGenericType(typeof(TService));
            var servicesObject = injector.GetService(genericEnumerable);
            var servicesEnumerable = (IEnumerable<object>)servicesObject;
            var strongEnumerable = servicesEnumerable.Cast<TService>();
            return strongEnumerable;
        }
    }
}
