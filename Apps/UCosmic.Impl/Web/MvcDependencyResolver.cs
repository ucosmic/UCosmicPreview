using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ServiceLocatorPattern;

namespace UCosmic.Impl
{
    public class MvcDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public MvcDependencyResolver()
        {
            _serviceProvider = ServiceProviderLocator.Current;
        }

        public object GetService(Type serviceType)
        {
            return _serviceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var services = _serviceProvider.GetService(typeof(IEnumerable<>)
                .MakeGenericType(serviceType)) as IEnumerable<object>;
            return services ?? Enumerable.Empty<object>();
        }
    }
}