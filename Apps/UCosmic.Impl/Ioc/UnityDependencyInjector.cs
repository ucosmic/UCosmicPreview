using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace UCosmic
{
    public class UnityDependencyInjector : IInjectDependencies
    {
        private readonly IUnityContainer _container;

        public UnityDependencyInjector()
        {
            _container = new UnityContainer().LoadConfiguration();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                if (!_container.IsRegistered(serviceType))
                {
                    if (serviceType.IsAbstract || serviceType.IsInterface)
                    {
                        return null;
                    }
                }
                return _container.Resolve(serviceType);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (Exception)
            {
                return Enumerable.Empty<object>();
            }
        }
    }


}