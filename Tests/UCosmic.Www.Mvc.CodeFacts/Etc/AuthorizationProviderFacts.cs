using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc
{
    // ReSharper disable PossibleNullReferenceException
    // ReSharper disable UnusedMember.Global
    public class AuthorizationProviderFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenNullArgIsPassed()
            {
                ArgumentNullException exception = null;
                try
                {
                    new AuthorizationProvider(null);
                }
                catch (ArgumentNullException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                exception.ParamName.ShouldEqual("roles");
            }
        }
    }
    // ReSharper restore PossibleNullReferenceException
}
