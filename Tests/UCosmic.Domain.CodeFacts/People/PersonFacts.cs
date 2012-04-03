using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class PersonFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class AffiliateWithMethod
        {
            [TestMethod]
            public void ReturnsExistingAffiliation_WhenAlreadyAffiliated()
            {
                var establishment = new Establishment();
                var affiliation = new Affiliation { Establishment = establishment };
                var person = new Person
                {
                    Affiliations = new List<Affiliation> { affiliation }
                };

                var result = person.AffiliateWith(establishment);

                result.ShouldEqual(affiliation);
            }
        }

        [TestClass]
        public class UserProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new User();
                var entity = new Person { User = value };
                entity.ShouldNotBeNull();
                entity.User.ShouldEqual(value);

            }

            [TestMethod]
            public void IsVirtual()
            {
                new PersonRuntimeEntity();
            }
            private class PersonRuntimeEntity : Person
            {
                public override User User
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class AffiliationsProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new List<Affiliation> { new Affiliation(), new Affiliation() };
                var entity = new Person { Affiliations = value };
                entity.ShouldNotBeNull();
                entity.Affiliations.ShouldEqual(value);

            }

            [TestMethod]
            public void IsVirtual()
            {
                new PersonRuntimeEntity();
            }
            private class PersonRuntimeEntity : Person
            {
                public override ICollection<Affiliation> Affiliations
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class EmailsProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new List<EmailAddress> { new EmailAddress(), new EmailAddress() };
                var entity = new Person { Emails = value };
                entity.ShouldNotBeNull();
                entity.Emails.ShouldEqual(value);

            }

            [TestMethod]
            public void IsVirtual()
            {
                new PersonRuntimeEntity();
            }
            private class PersonRuntimeEntity : Person
            {
                public override ICollection<EmailAddress> Emails
                {
                    get { return null; }
                    set { }
                }
            }
        }
    }
}
