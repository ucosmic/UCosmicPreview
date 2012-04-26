using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Web;
using SimpleInjector;

namespace UCosmic.Impl
{
    public static class SimpleHttpContextLifestyleExtensions
    {
        [DebuggerStepThrough]
        public static void RegisterPerWebRequest<TService, TImplementation>(
            this Container container)
            where TService : class
            where TImplementation : class, TService
        {
            Func<TService> instanceCreator = container.GetInstance<TImplementation>;
            container.RegisterPerWebRequest(instanceCreator);
        }

        [DebuggerStepThrough]
        public static void RegisterPerWebRequest<TService>(
            this Container container,
            Func<TService> instanceCreator) where TService : class
        {
            var creator = new PerWebRequestInstanceCreator<TService>(instanceCreator);
            container.Register(creator.GetInstance);
        }

        [DebuggerStepThrough]
        public static void RegisterPerWebRequest<TConcrete>(this Container container)
            where TConcrete : class
        {
            container.Register<TConcrete>();

            container.ExpressionBuilt += (sender, e) =>
            {
                if (e.RegisteredServiceType != typeof(TConcrete)) return;

                var transientInstanceCreator = Expression.Lambda<Func<TConcrete>>(
                    e.Expression, new ParameterExpression[0]).Compile();

                var creator = new PerWebRequestInstanceCreator<TConcrete>(
                    transientInstanceCreator);

                e.Expression = Expression.Call(Expression.Constant(creator),
                    creator.GetType().GetMethod("GetInstance"));
            };
        }

        [DebuggerStepThrough]
        public static void DisposeInstance<TService>() where TService : class
        {
            IDisposable disposable = null;
            var httpContext = HttpContext.Current;

            if (httpContext != null)
            {
                var key = typeof(PerWebRequestInstanceCreator<TService>);
                if (httpContext.Items.Contains(key))
                {
                    disposable = httpContext.Items[key] as IDisposable;
                    httpContext.Items.Remove(key);
                }
            }
            else
            {
                disposable = PerWebRequestInstanceCreator<TService>
                    .ThreadStaticInstance as IDisposable;
                PerWebRequestInstanceCreator<TService>
                    .ThreadStaticInstance = null;
            }

            if (disposable != null) disposable.Dispose();
        }

        private sealed class PerWebRequestInstanceCreator<T> where T : class
        {
            private readonly Func<T> _instanceCreator;

            internal PerWebRequestInstanceCreator(Func<T> instanceCreator)
            {
                _instanceCreator = instanceCreator;
            }

            [ThreadStatic]
            internal static T ThreadStaticInstance;

            [DebuggerStepThrough]
            public T GetInstance()
            {
                var context = HttpContext.Current;

                // when there is no HttpContext, fall back to per-thread instance creator
                if (context == null)
                    return ThreadStaticInstance
                        ?? (ThreadStaticInstance = _instanceCreator());

                object key = GetType();

                var instance = (T)context.Items[key];

                if (instance == null)
                    context.Items[key] = instance = _instanceCreator();

                return instance;
            }
        }
    }
}
