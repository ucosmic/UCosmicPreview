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
                var token = Guid.NewGuid();
                const string intent = "confirmation intent";
                const string secretCode = "its a secret";
                var emailAddress = new EmailAddress
                {
                    IsConfirmed = true,
                    Confirmations = new List<EmailConfirmation>
                        {
                            new EmailConfirmation
                            {
                                Token = token,
                                Intent = intent,
                                SecretCode = secretCode,
                                ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                                ConfirmedOnUtc = null,
                            }
                        },
                };

                // act
                var result = emailAddress.Confirm(token, intent, secretCode);

                // assert
                result.ShouldBeTrue();
            }

            [TestMethod]
            public void UpdatesConfirmedOnUtc_WhenEmailAddressIsAlreadyConfirmed()
            {
                // arrange
                var token = Guid.NewGuid();
                const string intent = "confirmation intent";
                const string secretCode = "its a secret";
                var emailAddress = new EmailAddress
                {
                    IsConfirmed = true,
                    Confirmations = new List<EmailConfirmation>
                        {
                            new EmailConfirmation
                            {
                                Token = token,
                                Intent = intent,
                                SecretCode = secretCode,
                                ExpiresOnUtc = DateTime.UtcNow.Add(new TimeSpan(0, 1, 0)),
                                ConfirmedOnUtc = null,
                            }
                        },
                };

                // act
                emailAddress.Confirm(token, intent, secretCode);

                // assert
                var confirmation = emailAddress.Confirmations.Single(c => c.Token == token);
                confirmation.ConfirmedOnUtc.HasValue.ShouldBeTrue();
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
                    set { }
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
                    set { }
                }
            }
        }

    }
}
