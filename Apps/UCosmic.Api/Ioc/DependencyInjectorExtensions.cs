using System.Collections.Generic;
using System.Linq;

namespace UCosmic
{
    public static class DependencyInjectorExtensions
    {
        public static TService GetService<TService>(this IInjectDependencies resolver)
        {
            return (TService)resolver.GetService(typeof(TService));
        }

        public static IEnumerable<TService> GetServices<TService>(this IInjectDependencies resolver)
        {
            return resolver.GetServices(typeof(TService)).Cast<TService>();
        }
    }
}
