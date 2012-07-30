using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public static class IdentityProviderListItemFacts
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

        public static class TheEmailDomainInfoClass
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
