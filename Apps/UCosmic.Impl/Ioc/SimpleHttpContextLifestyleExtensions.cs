using System;
using System.Diagnostics;
using System.Web;
using SimpleInjector;

namespace UCosmic
{
    public static class SimpleHttpContextLifestyleExtensions
    {
        public static void RegisterPerWebRequest<TService, TImplementation>(
            this Container container)
            where TService : class
            where TImplementation : class, TService
        {
            Func<TService> instanceCreator = container.GetInstance<TImplementation>;
            container.RegisterPerWebRequest(instanceCreator);
        }

        private static void RegisterPerWebRequest<TService>(
            this Container container,
            Func<TService> instanceCreator) where TService : class
        {
            var creator = new PerWebRequestInstanceCreator<TService>(instanceCreator);
            container.Register(creator.GetInstance);
        }

        public static void DisposeInstance<TService>() where TService : class
        {
            object key = typeof(PerWebRequestInstanceCreator<TService>);
            var context = HttpContext.Current;
            if (context == null || !context.Items.Contains(key)) return;

            var instance = HttpContext.Current.Items[key] as IDisposable;
            context.Items.Remove(key);
            if (instance == null) return;
            instance.Dispose();
        }

        private sealed class PerWebRequestInstanceCreator<T> where T : class
        {
            private readonly Func<T> _instanceCreator;

            internal PerWebRequestInstanceCreator(Func<T> instanceCreator)
            {
                _instanceCreator = instanceCreator;
            }

            [DebuggerStepThrough]
            internal T GetInstance()
            {
                var context = HttpContext.Current;

                if (context == null)
                {
                    // No HttpContext: Let's create a transient object.
                    return _instanceCreator();
                }

                var key = GetType();

                var instance = (T)context.Items[key];

                if (instance == null)
                {
                    context.Items[key] = instance = _instanceCreator();
                }

                return instance;
            }
        }
    }
}
