using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Saml
{
    // ReSharper disable UnusedMember.Global
    public class Saml2ResponseFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class ServiceProviderBindingProperty
        {
            [TestMethod]
            [ExpectedException(typeof(InvalidCastException))]
            public void HasPublicGet()
            {
                var response = (Saml2Response)new object();
                var serviceProviderBinding = response.ServiceProviderBinding;
                serviceProviderBinding.ShouldBeNull();
            }
        }

        [TestClass]
        public class ServiceIsSignedProperty
        {
            [TestMethod]
            [ExpectedException(typeof(InvalidCastException))]
            public void HasPublicGet()
            {
                var response = (Saml2Response)new object();
                var isSigned = response.IsSigned;
                isSigned.ShouldBeNull();
            }
        }
    }
}
