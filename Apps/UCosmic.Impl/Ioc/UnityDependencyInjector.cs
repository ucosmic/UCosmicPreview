using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using UCosmic.Domain;
using Unity.AutoRegistration;
using UCosmic.Domain.Establishments;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace UCosmic
{
    public class UnityDependencyInjector : IInjectDependencies
    {
        internal readonly IUnityContainer Container;

        public UnityDependencyInjector()
        {
            Container = new UnityContainer().LoadConfiguration();

            Container.AddExtension(new DecoratorContainerExtension());

            Action<Type, IUnityContainer> registrator = (t, c) =>
            { // set breakpoint here
                c.RegisterType(typeof (IHandleCommands<>), t);
            };

            Container.ConfigureAutoRegistration()
                .ExcludeSystemAssemblies()
                .ExcludeAssemblies(a => a.GetName().FullName.Contains("Microsoft.Web.Mvc"))
                .Include(type => type.ImplementsOpenGeneric(typeof(IHandleCommands<>)), 
                    registrator
                )
                .ApplyAutoRegistration()
            ;

            var aService = Container.Resolve<IHandleCommands<DoSomethingCommand>>();
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

    public class DecoratorContainerExtension : UnityContainerExtension
    {
        private Dictionary<Type, List<Type>> _typeStacks;
        protected override void Initialize()
        {
            _typeStacks = new Dictionary<Type, List<Type>>();
            Context.Registering += AddRegistration;

            Context.Strategies.Add(
                new DecoratorBuildStrategy(_typeStacks),
                UnityBuildStage.PreCreation
            );
        }

        private void AddRegistration(object sender, RegisterEventArgs e)
        {
            if (!e.TypeFrom.IsInterface)
            {
                return;
            }

            List<Type> stack = null;
            if (!_typeStacks.ContainsKey(e.TypeFrom))
            {
                stack = new List<Type>();
                _typeStacks.Add(e.TypeFrom, stack);
            }
            else
            {
                stack = _typeStacks[e.TypeFrom];
            }

            stack.Add(e.TypeTo);
        }
    }

    public class DecoratorBuildStrategy : BuilderStrategy
    {
        private readonly Dictionary<Type, List<Type>> _typeStacks;

        public DecoratorBuildStrategy(Dictionary<Type, List<Type>> typeStacks)
        {
            _typeStacks = typeStacks;
        }

        public override void PreBuildUp(IBuilderContext context)
        {
            var key = context.OriginalBuildKey;

            if (!(key.Type.IsInterface && _typeStacks.ContainsKey(key.Type)))
            {
                return;
            }

            if (null != context.GetOverriddenResolver(key.Type))
            {
                return;
            }

            Stack<Type> stack = new Stack<Type>(
                _typeStacks[key.Type]
                );

            object value = null;
            stack.ForEach(
                t =>
                {
                    value = context.NewBuildUp(
                        new NamedTypeBuildKey(t, key.Name)
                    );
                    var overrides = new DependencyOverride(
                        key.Type,
                        value
                     );
                    context.AddResolverOverrides(overrides);
                }
            );

            context.Existing = value;
            context.BuildComplete = true;
        }
    }
}