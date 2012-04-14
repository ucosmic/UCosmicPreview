using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using SimpleInjector;

namespace UCosmic
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

            //// converted to foreach loop
            //for (var index = 0; index < filters.Length; index++)
            //    _container.InjectProperties(filters[index].Instance);

            foreach (var filter in filters)
                _container.InjectProperties(filter.Instance);

            return filters;
        }
    }
}
