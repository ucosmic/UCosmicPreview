using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using SimpleInjector;

namespace UCosmic.Impl
{
    public sealed class SimpleFilterAttributeFilterProvider : FilterAttributeFilterProvider
    {
        private readonly Container _container;

        public SimpleFilterAttributeFilterProvider(Container container)
            : base(false)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(controllerContext, actionDescriptor).ToArray();

            foreach (var filter in filters)
                _container.InjectProperties(filter.Instance);

            return filters;
        }
    }
}
