using System;

namespace UCosmic.IoC
{
    // NOTE: Service Locator pattern should be avoided in most places. Try to use constructor injection instead.
    public static class DependencyInjector
    {
        private static IServiceProvider _current = new DefaultDependencyInjector();

        public static void Set(IServiceProvider injector)
        {
            if (injector == null) throw new ArgumentNullException("injector");
            _current = injector;
        }

        public static IServiceProvider Current
        {
            get { return _current; }
        }

        private class DefaultDependencyInjector : IServiceProvider
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
        }
    }
}
