using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic
{
    // NOTE: Service Locator pattern should be avoided in most places. Try to use constructor injection instead.
    public class DependencyInjector
    {
        private IInjectDependencies _current = new DefaultDependencyInjector();
        private static readonly DependencyInjector Instance = new DependencyInjector();

        static DependencyInjector()
        {
            Instance = new DependencyInjector();
        }

        private DependencyInjector()
        {
            _current = new DefaultDependencyInjector();
        }

        public static void SetInjector(IInjectDependencies injector)
        {
            Instance.InnerSetInjector(injector);
        }

        private void InnerSetInjector(IInjectDependencies injector)
        {
            if (injector == null) throw new ArgumentNullException("injector");
            _current = injector;
        }

        public static IInjectDependencies Current
        {
            get { return Instance.InnerCurrent; }
        }

        private IInjectDependencies InnerCurrent
        {
            get { return _current; }
        }

        private class DefaultDependencyInjector : IInjectDependencies
        {
            public object GetService(Type serviceType)
            {
                try
                {
                    return Activator.CreateInstance(serviceType);
                }
                catch
                {
                    return null;
                }
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return Enumerable.Empty<object>();
            }
        }
    }
}
