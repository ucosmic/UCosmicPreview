using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            public void HasPublicGet()
            {
                var response = new Mock<Saml2Response>();
                var serviceProviderBinding = response.Object.ServiceProviderBinding;
                serviceProviderBinding.ShouldEqual(Saml2SsoBinding.NotSpecified);
            }
        }

        [TestClass]
        public class ServiceIsSignedProperty
        {
            [TestMethod]
            public void HasPublicGet()
            {
                var response = new Mock<Saml2Response>();
                var isSigned = response.Object.IsSigned;
                isSigned.ShouldBeFalse();
            }
        }
    }
}
