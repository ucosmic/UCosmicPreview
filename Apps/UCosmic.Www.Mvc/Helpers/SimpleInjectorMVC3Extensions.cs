namespace UCosmic.Www.Mvc.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Extensions;

    /// <summary>
    /// Extension methods for MVC3.
    /// </summary>
    public static class SimpleInjectorMVC3Extensions
    {
        /// <summary>Registers the Simple Injector container as Dependency Resolver.</summary>
        /// <param name="container">The container.</param>    
        public static void RegisterAsMvcDependencyResolver(this Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            DependencyResolver.SetResolver(new SimpleInjectionDependencyResolver { Container = container });
        }

        /// <summary>Allow the Simple Injector to inject properties into MVC filter attributes.</summary>
        /// <param name="container">The container.</param>
        public static void RegisterAttributeFilterProvider(this Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            var providers = FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().ToList();

            providers.ForEach(provider => FilterProviders.Providers.Remove(provider));

            FilterProviders.Providers.Add(new SimpleInjectorFilterAttributeFilterProvider(container));
        }

        [DebuggerStepThrough]
        public static void RegisterControllers(this Container container, params Assembly[] assemblies)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            if (assemblies == null)
            {
                throw new ArgumentNullException("assemblies");
            }

            var controllerTypes =
                from assembly in assemblies
                from type in assembly.GetExportedTypes()
                where type.Name.EndsWith("Controller")
                where typeof(IController).IsAssignableFrom(type)
                where !type.IsAbstract
                select type;

            foreach (var controllerType in controllerTypes)
            {
                container.Register(controllerType);
            }
        }

        private sealed class SimpleInjectionDependencyResolver : IDependencyResolver
        {
            public Container Container { get; set; }

            //[DebuggerStepThrough]
            public object GetService(Type serviceType)
            {
                return ((IServiceProvider)this.Container).GetService(serviceType);
            }

            [DebuggerStepThrough]
            public IEnumerable<object> GetServices(Type serviceType)
            {
                return this.Container.GetAllInstances(serviceType);
            }
        }

        public sealed class SimpleInjectorFilterAttributeFilterProvider : FilterAttributeFilterProvider
        {
            private readonly Container container;

            public SimpleInjectorFilterAttributeFilterProvider(Container container)
                : base(false)
            {
                this.container = container;
            }

            //[DebuggerStepThrough]
            public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext,
                ActionDescriptor actionDescriptor)
            {
                var filters = base.GetFilters(controllerContext, actionDescriptor).ToArray();

                for (int index = 0; index < filters.Length; index++)
                {
                    this.container.InjectProperties(filters[index].Instance);
                }

                return filters;
            }
        }
    }
}