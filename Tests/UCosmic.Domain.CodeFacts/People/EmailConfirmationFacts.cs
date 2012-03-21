using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class EmailConfirmationFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class IdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 2;
                var entity = new EmailConfirmation { Id = value };
                entity.ShouldNotBeNull();
                entity.Id.ShouldEqual(value);
            }
        }

        [TestClass]
        public class EmailAddressIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 0;
                var entity = new EmailConfirmation { EmailAddressId = value };
                entity.ShouldNotBeNull();
                entity.EmailAddressId.ShouldEqual(value);
            }
        }

        [TestClass]
        public class EmailAddressProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EmailConfirmationRuntimeEntity();
            }
            private class EmailConfirmationRuntimeEntity : EmailConfirmation
            {
                public override EmailAddress EmailAddress
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class IssuedOnUtcProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = DateTime.UtcNow;
                var entity = new EmailConfirmation { IssuedOnUtc = value };
                entity.ShouldNotBeNull();
                entity.IssuedOnUtc.ShouldEqual(value);
            }
        }
    }

}
