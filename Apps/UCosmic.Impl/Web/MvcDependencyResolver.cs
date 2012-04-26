using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace UCosmic.Impl
{
    public class MvcDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider _injector;

        public MvcDependencyResolver()
        {
            _injector = DependencyInjector.Current;
        }

        public object GetService(Type serviceType)
        {
            return _injector.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var services = _injector.GetService(typeof(IEnumerable<>).MakeGenericType(serviceType)) as IEnumerable<object>;
            return services ?? Enumerable.Empty<object>();
        }
    }
}