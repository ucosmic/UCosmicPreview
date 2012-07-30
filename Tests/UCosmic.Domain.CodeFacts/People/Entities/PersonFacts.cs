using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public static class PersonFacts
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void InitializesCollection_ForAffiliations()
            {
                var person = new Person();

                person.ShouldNotBeNull();
                person.Affiliations.ShouldNotBeNull();
                person.Affiliations.Count.ShouldEqual(0);
            }

            [TestMethod]
            public void InitializesCollection_ForEmails()
            {
                var person = new Person();

                person.ShouldNotBeNull();
                person.Emails.ShouldNotBeNull();
                person.Emails.Count.ShouldEqual(0);
            }

            [TestMethod]
            public void InitializesCollection_ForMessages()
            {
                var person = new Person();

                person.ShouldNotBeNull();
                person.Messages.ShouldNotBeNull();
                person.Messages.Count.ShouldEqual(0);
            }
        }

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
                    protected internal set { }
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
                    protected internal set { }
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
                    protected internal set { }
                }
            }
        }

        [TestClass]
        public class MessagesProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new EmailMessage[] { };
                var entity = new Person { Messages = value };
                entity.ShouldNotBeNull();
                entity.Messages.ShouldEqual(value);
                entity.Messages.Count.ShouldEqual(0);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new PersonRuntimeEntity();
            }
            private class PersonRuntimeEntity : Person
            {
                public override ICollection<EmailMessage> Messages
                {
                    get { return null; }
                    protected internal set { }
                }
            }
        }
    }
}
