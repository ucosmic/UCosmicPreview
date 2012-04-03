using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignOn
{
    // ReSharper disable UnusedMember.Global
    public class Saml2IntegrationInfoFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheOfficialNameProperty
        {
            [TestMethod]
            public void HasPublicSetter()
            {
                new Saml2IntegrationInfo
                {
                    OfficialName = "official name"
                };
            }
        }

        [TestClass]
        public class TheEmailDomainsProperty
        {
            [TestMethod]
            public void HasPublicSetter()
            {
                new Saml2IntegrationInfo
                {
                    EmailDomains = null
                };
            }
        }

        // ReSharper disable UnusedMember.Global
        public class TheEmailDomainInfoClass
        // ReSharper restore UnusedMember.Global
        {
            [TestClass]
            public class TheValueProperty
            {
                [TestMethod]
                public void HasPublicSetter()
                {
                    new Saml2IntegrationInfo.EmailDomainInfo
                    {
                        Value = "@domain.tld"
                    };
                }
            }
        }
    }
}
