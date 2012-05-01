using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UCosmic.Www.Mvc.Areas.Saml.Models
{
    // ReSharper disable UnusedMember.Global
    public class IdentityProviderListItemFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheOfficialNameProperty
        {
            [TestMethod]
            public void HasPublicSetter()
            {
                new IdentityProviderListItem
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
                new IdentityProviderListItem
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
                    new IdentityProviderListItem.EmailDomain
                    {
                        Value = "@domain.tld"
                    };
                }
            }
        }
    }
}
