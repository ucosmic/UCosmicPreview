using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Email;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class EmailMessageFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class IdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 4;
                var entity = new EmailMessage { Id = value };
                entity.ShouldNotBeNull();
                entity.Id.ShouldEqual(value);
            }
        }

        [TestClass]
        public class EntityIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = Guid.NewGuid();
                var entity = new EmailMessage { EntityId = value };
                entity.ShouldNotBeNull();
                entity.EntityId.ShouldEqual(value);
            }
        }

        [TestClass]
        public class ReplyToDisplayNameProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const string value = "text";
                var entity = new EmailMessage { ReplyToDisplayName = value };
                entity.ShouldNotBeNull();
                entity.ReplyToDisplayName.ShouldEqual(value);
            }
        }

        [TestClass]
        public class ComposedByPrincipalProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const string value = "text";
                var entity = new EmailMessage { ComposedByPrincipal = value };
                entity.ShouldNotBeNull();
                entity.ComposedByPrincipal.ShouldEqual(value);
            }
        }

        [TestClass]
        public class ComposedOnUtcProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = DateTime.UtcNow;
                var entity = new EmailMessage { ComposedOnUtc = value };
                entity.ShouldNotBeNull();
                entity.ComposedOnUtc.ShouldEqual(value);
            }
        }

        [TestClass]
        public class FromEmailTemplateIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 20;
                var entity = new EmailMessage { FromEmailTemplateId = value };
                entity.ShouldNotBeNull();
                entity.FromEmailTemplateId.ShouldEqual(value);
            }
        }

        [TestClass]
        public class FromEmailTemplateProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EmailMessageRuntimeEntity();
            }
            private class EmailMessageRuntimeEntity : EmailMessage
            {
                public override EmailTemplate FromEmailTemplate
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class ToEmailAddressIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 64;
                var entity = new EmailMessage { ToEmailAddressId = value };
                entity.ShouldNotBeNull();
                entity.ToEmailAddressId.ShouldEqual(value);
            }
        }

        [TestClass]
        public class ToProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EmailMessageRuntimeEntity();
            }
            private class EmailMessageRuntimeEntity : EmailMessage
            {
                public override EmailAddress To
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class IsArchivedProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const bool value = true;
                var entity = new EmailMessage { IsArchived = value };
                entity.ShouldNotBeNull();
                entity.IsArchived.ShouldEqual(value);
            }
        }

        [TestClass]
        public class IsDeletedProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const bool value = true;
                var entity = new EmailMessage { IsDeleted = value };
                entity.ShouldNotBeNull();
                entity.IsDeleted.ShouldEqual(value);
            }
        }

    }
}
