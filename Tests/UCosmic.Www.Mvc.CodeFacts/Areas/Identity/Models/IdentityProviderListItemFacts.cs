using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

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
                var obj = new IdentityProviderListItem
                {
                    OfficialName = "official name"
                };
                obj.ShouldNotBeNull();
            }
        }

        [TestClass]
        public class TheEmailDomainsProperty
        {
            [TestMethod]
            public void HasPublicSetter()
            {
                var obj = new IdentityProviderListItem
                {
                    EmailDomains = null
                };
                obj.ShouldNotBeNull();
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
                    var obj = new IdentityProviderListItem.EmailDomain
                    {
                        Value = "@domain.tld"
                    };
                    obj.ShouldNotBeNull();
                }
            }
        }
    }
}
