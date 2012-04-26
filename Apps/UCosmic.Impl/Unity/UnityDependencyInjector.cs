using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace UCosmic.Impl
{
    public class UnityDependencyInjector : IServiceProvider
    {
        internal readonly IUnityContainer Container;

        public UnityDependencyInjector()
        {
            Container = new UnityContainer().LoadConfiguration();
        }

        public object GetService(Type serviceType)
        {
            if (!Container.IsRegistered(serviceType))
            {
                if (serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    var all = Container.ResolveAll(serviceType.GetGenericArguments()[0]).ToList();
                    if (all.Any()) return all;
                }
                if (serviceType.IsAbstract || serviceType.IsInterface)
                {
                    return null;
                }
            }
            return Container.Resolve(serviceType);
        }
    }
}