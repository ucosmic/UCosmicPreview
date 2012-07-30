using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Establishments
{
    public static class EstablishmentFacts
    {
        [TestClass]
        public class TranslateNameToMethod
        {
            [TestMethod]
            public void ReturnsNull_WhenNoNamesExist()
            {
                var establishment = new Establishment { Names = new List<EstablishmentName>() };
                var translatedName = establishment.TranslateNameTo("en");
                translatedName.ShouldBeNull();
            }
        }

        [TestClass]
        public class AffiliatesProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new List<Affiliation> { new Affiliation() };
                var entity = new Establishment { Affiliates = value };
                entity.ShouldNotBeNull();
                entity.Affiliates.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new EstablishmentRuntimeEntity();
            }
            private class EstablishmentRuntimeEntity : Establishment
            {
                public override ICollection<Affiliation> Affiliates
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class PartnerContactInfoProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new EstablishmentContactInfo();
                var entity = new Establishment { PartnerContactInfo = value };
                entity.ShouldNotBeNull();
                entity.PartnerContactInfo.ShouldEqual(value);
            }
        }

        [TestClass]
        public class NamesProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EstablishmentRuntimeEntity();
            }
            private class EstablishmentRuntimeEntity : Establishment
            {
                public override ICollection<EstablishmentName> Names
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class UrlsProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EstablishmentRuntimeEntity();
            }
            private class EstablishmentRuntimeEntity : Establishment
            {
                public override ICollection<EstablishmentUrl> Urls
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class ParentProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EstablishmentRuntimeEntity();
            }
            private class EstablishmentRuntimeEntity : Establishment
            {
                public override Establishment Parent
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class ChildrenProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EstablishmentRuntimeEntity();
            }
            private class EstablishmentRuntimeEntity : Establishment
            {
                public override ICollection<Establishment> Children
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class AncestorsProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EstablishmentRuntimeEntity();
            }
            private class EstablishmentRuntimeEntity : Establishment
            {
                public override ICollection<EstablishmentNode> Ancestors
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class OffspringProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EstablishmentRuntimeEntity();
            }
            private class EstablishmentRuntimeEntity : Establishment
            {
                public override ICollection<EstablishmentNode> Offspring
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class LocationProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EstablishmentRuntimeEntity();
            }
            private class EstablishmentRuntimeEntity : Establishment
            {
                public override EstablishmentLocation Location
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class SamlSignOnProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EstablishmentRuntimeEntity();
            }
            private class EstablishmentRuntimeEntity : Establishment
            {
                public override EstablishmentSamlSignOn SamlSignOn
                {
                    get { return null; }
                    protected internal set { }
                }
            }
        }

        [TestClass]
        public class TypeProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EstablishmentRuntimeEntity();
            }
            private class EstablishmentRuntimeEntity : Establishment
            {
                public override EstablishmentType Type
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class EmailDomainsProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EstablishmentRuntimeEntity();
            }
            private class EstablishmentRuntimeEntity : Establishment
            {
                public override ICollection<EstablishmentEmailDomain> EmailDomains
                {
                    get { return null; }
                    set { }
                }
            }
        }
    }
}
