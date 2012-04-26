using System;
using System.Collections.Generic;
using System.Linq;
using SimpleInjector;
using System.Linq.Expressions;

namespace UCosmic.Impl
{
    public static class SimpleDecoratorExtensions
    {
        public static void RegisterRunAsyncCommandHandlerProxy<TCommand>(this Container container, bool saveChangesAndDispose = true)
        {
            var commandType = typeof(TCommand);
            var openCommandHandlerType = typeof(IHandleCommands<>);
            var closedCommandHandlerType = openCommandHandlerType.MakeGenericType(commandType);

            // optionally wrap the handler in a SaveChangesAndDispose decorator
            if (saveChangesAndDispose)
                container.RegisterGenericDecorator(openCommandHandlerType, typeof(SaveChangesAndDisposeDecorator<>),
                    c => c.ServiceType == closedCommandHandlerType);

            container.ExpressionBuilt += (s, e) =>
            {
                if (e.RegisteredServiceType != closedCommandHandlerType) return;

                // wrap the handler in a run async proxy
                var delegateType = typeof(Func<>).MakeGenericType(closedCommandHandlerType);
                var instanceCreator = Expression.Lambda(delegateType, e.Expression).Compile();
                var closedRunAsyncType = typeof(RunAsyncCommandHandlerProxy<>).MakeGenericType(commandType);
                var runAsyncConstructor = closedRunAsyncType.GetConstructors().Single();
                e.Expression = Expression.New(runAsyncConstructor, Expression.Constant(instanceCreator));
            };
        }

        public static void RegisterGenericDecorator(this Container container,
            Type openGenericType, Type openGenericDecorator)
        {
            Func<SimpleDecoratorContext, bool> always = c => true;

            RegisterGenericDecorator(container, openGenericType,
                openGenericDecorator, always);
        }

        public static void RegisterGenericDecorator(this Container container,
            Type openGenericType, Type openGenericDecorator,
            Func<SimpleDecoratorContext, bool> predicate)
        {
            var interceptor = new DecoratorExpressionInterceptor
            {
                Container = container,
                OpenGenericType = openGenericType,
                OpenGenericDecorator = openGenericDecorator,
                Predicate = predicate
            };

            container.ExpressionBuilt += interceptor.Decorate;
        }

        // based on http://simpleinjector.codeplex.com/wikipage?title=DecoratorExtensions
        private sealed class DecoratorExpressionInterceptor
        {
            [ThreadStatic]
            private static Dictionary<Container, Dictionary<Type, ServiceTypeDecoratorInfo>> _serviceTypeInfos;

            public Container Container { private get; set; }

            public Type OpenGenericType { private get; set; }

            public Type OpenGenericDecorator { private get; set; }

            public Func<SimpleDecoratorContext, bool> Predicate { private get; set; }

            public void Decorate(object sender, ExpressionBuiltEventArgs e)
            {
                var serviceType = e.RegisteredServiceType;

                if (!serviceType.IsGenericType || serviceType.GetGenericTypeDefinition() != OpenGenericType ||
                    !Predicate(CreatePredicateContext(e))) return;

                var closedGenericDecorator = OpenGenericDecorator
                    .MakeGenericType(serviceType.GetGenericArguments());

                var ctor = closedGenericDecorator.GetConstructors().Single();

                var parameters =
                    from parameter in ctor.GetParameters()
                    let type = parameter.ParameterType
                    select type == serviceType 
                        ? e.Expression 
                        : Container.GetRegistration(type, true).BuildExpression();

                var expression = Expression.New(ctor, parameters);

                var info = GetServiceTypeInfo(e);

                info.AppliedDecorators.Add(closedGenericDecorator);

                e.Expression = expression;
            }

            private SimpleDecoratorContext CreatePredicateContext(ExpressionBuiltEventArgs e)
            {
                var info = GetServiceTypeInfo(e);

                return new SimpleDecoratorContext
                {
                    ServiceType = e.RegisteredServiceType,
                    Expression = e.Expression,
                    ImplementationType = info.ImplementationType,
                    AppliedDecorators = info.AppliedDecorators.ToArray()
                };
            }

            private ServiceTypeDecoratorInfo GetServiceTypeInfo(
                ExpressionBuiltEventArgs e)
            {
                var containerCache = _serviceTypeInfos;

                if (containerCache == null)
                {
                    containerCache = new Dictionary<Container,
                        Dictionary<Type, ServiceTypeDecoratorInfo>>();
                    _serviceTypeInfos = containerCache;
                }

                if (!containerCache.ContainsKey(Container))
                    containerCache[Container] =
                        new Dictionary<Type, ServiceTypeDecoratorInfo>();

                var cache = containerCache[Container];

                if (!cache.ContainsKey(e.RegisteredServiceType))
                    cache[e.RegisteredServiceType] =
                        new ServiceTypeDecoratorInfo(DetermineImplementationType(e));

                return cache[e.RegisteredServiceType];
            }

            private static Type DetermineImplementationType(ExpressionBuiltEventArgs e)
            {
                // singleton
                var constantExpression = e.Expression as ConstantExpression;
                if (constantExpression != null)
                    return (constantExpression).Value.GetType();

                // transient without initializers
                var newExpression = e.Expression as NewExpression;
                if (newExpression != null)
                    return (newExpression).Constructor.DeclaringType;

                // transient with initializers
                var invocation = e.Expression as InvocationExpression;
                if (invocation != null &&
                    invocation.Expression is ConstantExpression &&
                    invocation.Arguments.Count == 1 &&
                    invocation.Arguments[0] is NewExpression)
                    return ((NewExpression)invocation.Arguments[0])
                        .Constructor.DeclaringType;

                // implementation type can not be determined
                return e.RegisteredServiceType;
            }

            private sealed class ServiceTypeDecoratorInfo
            {
                public ServiceTypeDecoratorInfo(Type implementationType)
                {
                    ImplementationType = implementationType;
                    AppliedDecorators = new List<Type>();
                }

                public Type ImplementationType { get; private set; }

                public List<Type> AppliedDecorators { get; private set; }
            }
        }
    }
}