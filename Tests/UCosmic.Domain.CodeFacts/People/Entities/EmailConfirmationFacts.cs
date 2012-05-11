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
        public class TheConstructor
        {
            [TestMethod]
            public void SetsToken_ToNonEmptyGuid()
            {
                var entity = new EmailConfirmation(EmailConfirmationIntent.CreatePassword);
                entity.ShouldNotBeNull();
                entity.Token.ShouldNotEqual(Guid.Empty);
            }

            [TestMethod]
            public void SetsIssuedOnUtc_ToUtcNow()
            {
                var entity = new EmailConfirmation(EmailConfirmationIntent.ResetPassword);
                entity.ShouldNotBeNull();
                entity.IssuedOnUtc.ShouldBeInRange(
                    DateTime.UtcNow.AddSeconds(-5),
                    DateTime.UtcNow.AddSeconds(5));
            }

            [TestMethod]
            public void SetsExpiresOnUtc_ToUtcNow_PlusTwoHours()
            {
                var entity = new EmailConfirmation(EmailConfirmationIntent.CreatePassword);
                entity.ShouldNotBeNull();
                entity.ExpiresOnUtc.ShouldBeInRange(
                    DateTime.UtcNow.AddSeconds((60 * 60 * 2) - 5),
                    DateTime.UtcNow.AddSeconds((60 * 60 * 2) + 5));
            }
        }

        [TestClass]
        public class TheIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 2;
                var entity = new EmailConfirmation(EmailConfirmationIntent.ResetPassword) { Id = value };
                entity.ShouldNotBeNull();
                entity.Id.ShouldEqual(value);
            }
        }

        [TestClass]
        public class ThePersonIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 0;
                var entity = new EmailConfirmation(EmailConfirmationIntent.CreatePassword) { PersonId = value };
                entity.ShouldNotBeNull();
                entity.PersonId.ShouldEqual(value);
            }
        }

        [TestClass]
        public class TheEmailAddressProperty
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
                    protected internal set { }
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
                var entity = new EmailConfirmation(EmailConfirmationIntent.ResetPassword) { IssuedOnUtc = value };
                entity.ShouldNotBeNull();
                entity.IssuedOnUtc.ShouldEqual(value);
            }
        }
    }

}
