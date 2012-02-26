using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace UCosmic
{
    public class MvcDependencyResolver : IDependencyResolver
    {
        private readonly IInjectDependencies _injector;

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
            return _injector.GetServices(serviceType);
        }
    }
}