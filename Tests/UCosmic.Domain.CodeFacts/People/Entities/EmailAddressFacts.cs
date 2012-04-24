using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class EmailAddressFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class PersonIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 8;
                var entity = new EmailAddress { PersonId = value };
                entity.ShouldNotBeNull();
                entity.PersonId.ShouldEqual(value);
            }
        }

        [TestClass]
        public class ConfirmMethod
        {
            [TestMethod]
            public void ReturnsFalse_WhenTokenIsEmptyGuid()
            {
                // arrange
                var emailAddress = new EmailAddress();

                // act
                var result = emailAddress.Confirm(Guid.Empty, null, null);

                // assert
                result.ShouldBeFalse();
            }

            [TestMethod]
            public void ReturnsTrue_WhenEmailAddressIsAlreadyConfirmed()
            {
                // arrange
                const string intent = "confirmation intent";
                const string secretCode = "its a secret";
                var confirmation = new EmailConfirmation
                {
                    Intent = intent,
                    SecretCode = secretCode,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    ConfirmedOnUtc = null,
                };
                var emailAddress = new EmailAddress
                {
                    IsConfirmed = true,
                    Confirmations = new[] { confirmation, },
                };

                // act
                var result = emailAddress.Confirm(confirmation.Token, intent, secretCode);

                // assert
                result.ShouldBeTrue();
            }

            [TestMethod]
            public void UpdatesConfirmedOnUtc_WhenEmailAddressIsAlreadyConfirmed()
            {
                // arrange
                const string intent = "confirmation intent";
                const string secretCode = "its a secret";
                var confirmation = new EmailConfirmation
                {
                    Intent = intent,
                    SecretCode = secretCode,
                    ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                    ConfirmedOnUtc = null,
                };
                var emailAddress = new EmailAddress
                {
                    IsConfirmed = true,
                    Confirmations = new[] { confirmation, },
                };

                // act
                emailAddress.Confirm(confirmation.Token, intent, secretCode);

                // assert
                var result = emailAddress.Confirmations.Single(c => c.Token == confirmation.Token);
                result.ConfirmedOnUtc.HasValue.ShouldBeTrue();
            }
        }

        [TestClass]
        public class PersonProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EmailAddressRuntimeEntity();
            }
            private class EmailAddressRuntimeEntity : EmailAddress
            {
                public override Person Person
                {
                    get { return null; }
                    protected internal set { }
                }
            }
        }

        [TestClass]
        public class ConfirmationsProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EmailAddressRuntimeEntity();
            }
            private class EmailAddressRuntimeEntity : EmailAddress
            {
                public override ICollection<EmailConfirmation> Confirmations
                {
                    get { return null; }
                    protected internal set { }
                }
            }
        }

    }
}
