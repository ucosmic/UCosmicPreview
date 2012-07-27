using System;
using System.Collections.Generic;
using MvcContrib.TestHelper;
using Rhino.Mocks;

namespace UCosmic.Www.Mvc
{
    public static class ReuseMock
    {
        private static readonly IDictionary<ControllerCustomization, TestControllerBuilder> TestControllerBuilders
            = new Dictionary<ControllerCustomization, TestControllerBuilder>();

        public static TestControllerBuilder TestControllerBuilder(ControllerCustomization customization = ControllerCustomization.None)
        {
            if (!TestControllerBuilders.ContainsKey(customization))
            {
                var testControllerBuilder = new TestControllerBuilder();

                switch (customization)
                {
                    case ControllerCustomization.ForUrlHelper:
                        testControllerBuilder.HttpContext.Response
                            .Stub(x => x.ApplyAppPathModifier(null))
                            .IgnoreArguments().Do(new Func<string, string>(s => s))
                            .Repeat.Any();
                        break;
                }


                TestControllerBuilders.Add(customization, testControllerBuilder);
            }

            var builder = TestControllerBuilders[customization];
            builder.HttpContext.User = null;

            builder.RouteData.DataTokens.Remove("ParentActionViewContext");

            return builder;
        }
    }

    public enum ControllerCustomization
    {
        None,
        ForUrlHelper,
    }
}
