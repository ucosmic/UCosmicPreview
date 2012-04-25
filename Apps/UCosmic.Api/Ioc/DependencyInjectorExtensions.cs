using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UCosmic
{
    public static class DependencyInjectorExtensions
    {
        [DebuggerStepThrough]
        public static TService GetService<TService>(this IServiceProvider injector)
        {
            return (TService)injector.GetService(typeof(TService));
        }

        [DebuggerStepThrough]
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
