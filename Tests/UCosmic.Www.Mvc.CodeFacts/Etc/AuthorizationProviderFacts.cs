using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Impl;

namespace UCosmic.Www.Mvc
{
    public class AuthorizationProviderFacts
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
                // ReSharper disable PossibleNullReferenceException
                exception.ParamName.ShouldEqual("roles");
                // ReSharper restore PossibleNullReferenceException
            }
        }
    }
}
