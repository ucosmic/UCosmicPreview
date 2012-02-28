using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace UCosmic
{
    public class UnityDependencyInjector : IInjectDependencies
    {
        internal readonly IUnityContainer Container;

        public UnityDependencyInjector()
        {
            Container = new UnityContainer().LoadConfiguration();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                if (!Container.IsRegistered(serviceType))
                {
                    if (serviceType.IsAbstract || serviceType.IsInterface)
                    {
                        return null;
                    }
                }
                return Container.Resolve(serviceType);

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
                return Container.ResolveAll(serviceType);
            }
            catch (Exception)
            {
                return Enumerable.Empty<object>();
            }
        }
    }


}