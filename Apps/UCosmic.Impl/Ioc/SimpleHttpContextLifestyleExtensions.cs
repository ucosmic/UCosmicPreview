using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Web;
using SimpleInjector;

namespace UCosmic
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

        // ReSharper disable MemberCanBePrivate.Global
        [DebuggerStepThrough]
        public static void RegisterPerWebRequest<TService>(
            this Container container,
            Func<TService> instanceCreator) where TService : class
        {
            var creator = new PerWebRequestInstanceCreator<TService>(instanceCreator);
            container.Register(creator.GetInstance);
        }
        // ReSharper restore MemberCanBePrivate.Global

        [DebuggerStepThrough]
        public static void RegisterPerWebRequest<TConcrete>(this Container container)
            where TConcrete : class
        {
            container.Register<TConcrete>();

            container.ExpressionBuilt += (sender, e) =>
            {
                if (e.RegisteredServiceType != typeof (TConcrete)) return;

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
            object key = typeof(PerWebRequestInstanceCreator<TService>);

            var instance = HttpContext.Current.Items[key] as IDisposable;

            if (instance != null)
            {
                instance.Dispose();
            }
        }

        private sealed class PerWebRequestInstanceCreator<T> where T : class
        {
            private readonly Func<T> _instanceCreator;

            internal PerWebRequestInstanceCreator(Func<T> instanceCreator)
            {
                _instanceCreator = instanceCreator;
            }

            [DebuggerStepThrough]
            public T GetInstance()
            {
                var context = HttpContext.Current;

                if (context == null)
                {
                    // No HttpContext: Let's create a transient object.
                    return _instanceCreator();
                }

                object key = GetType();

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
