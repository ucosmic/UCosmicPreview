using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class EmailMessageFacts
    // ReSharper restore UnusedMember.Global
    {
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
    }
}
