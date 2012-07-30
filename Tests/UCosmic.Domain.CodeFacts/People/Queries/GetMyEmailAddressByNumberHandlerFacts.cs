using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public static class GetMyEmailAddressByNumberHandlerFacts
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenQueryArgIsNull()
            {
                var handler = new GetMyEmailAddressByNumberHandler(null);
                ArgumentNullException exception = null;
                try
                {
                    handler.Handle(null);
                }
                catch (ArgumentNullException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.ParamName.ShouldEqual("query");
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void ExecutesQuery_ToGetUserByName()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var query = new GetMyEmailAddressByNumberQuery
                {
                    Principal = principal,
                };
                var emailAddress = new EmailAddress
                {
                    Person = new Person { User = new User { Name = principal.Identity.Name, }, },
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailAddress>()).Returns(new[] { emailAddress }.AsQueryable);
                var handler = new GetMyEmailAddressByNumberHandler(entities.Object);

                handler.Handle(query);

                entities.Verify(m => m.Query<EmailAddress>(), Times.Once());
            }

            [TestMethod]
            public void ThrowsNullReferenceException_WhenPrincipalIsNull()
            {
                var query = new GetMyEmailAddressByNumberQuery
                {
                    Principal = null,
                };
                var emailAddress = new EmailAddress
                {
                    Person = new Person { User = new User { Name = "", }, },
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailAddress>()).Returns(new[] { emailAddress }.AsQueryable);
                var handler = new GetMyEmailAddressByNumberHandler(entities.Object);
                NullReferenceException exception = null;

                try
                {
                    handler.Handle(query);
                }
                catch (NullReferenceException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
            }

            [TestMethod]
            public void ReturnsNull_WhenQueryForUser_ReturnsNull()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var query = new GetMyEmailAddressByNumberQuery
                {
                    Principal = principal,
                };
                var emailAddress = new EmailAddress
                {
                    Person = new Person { User = new User { Name = "someone else", }, },
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailAddress>()).Returns(new[] { emailAddress }.AsQueryable);
                var handler = new GetMyEmailAddressByNumberHandler(entities.Object);

                var result = handler.Handle(query);

                result.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsNull_WhenUserPerson_DoesNotHaveMatchingEmailNumber()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var query = new GetMyEmailAddressByNumberQuery
                {
                    Principal = principal,
                    Number = 1,
                };
                var emailAddresses = new[]
                {
                    new EmailAddress
                    {
                        Number = 2,
                        Person = new Person { User = new User { Name = principal.Identity.Name, }, },
                    },
                    new EmailAddress
                    {
                        Number = 3,
                        Person = new Person { User = new User { Name = principal.Identity.Name, }, },
                    },
                    new EmailAddress
                    {
                        Number = 4,
                        Person = new Person { User = new User { Name = principal.Identity.Name, }, },
                    },
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailAddress>()).Returns(emailAddresses.AsQueryable);
                var handler = new GetMyEmailAddressByNumberHandler(entities.Object);

                var result = handler.Handle(query);

                result.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsEmailAddress_WhenMatchingUserAndNumber_IsFound()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var query = new GetMyEmailAddressByNumberQuery
                {
                    Principal = principal,
                    Number = 1,
                };
                var emailAddresses = new[]
                {
                    new EmailAddress
                    {
                        Number = 1,
                        Person = new Person { User = new User { Name = principal.Identity.Name, }, },
                    },
                    new EmailAddress
                    {
                        Number = 3,
                        Person = new Person { User = new User { Name = principal.Identity.Name, }, },
                    },
                    new EmailAddress
                    {
                        Number = 4,
                        Person = new Person { User = new User { Name = principal.Identity.Name, }, },
                    },
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<EmailAddress>()).Returns(emailAddresses.AsQueryable);
                var handler = new GetMyEmailAddressByNumberHandler(entities.Object);

                var result = handler.Handle(query);

                result.ShouldNotBeNull();
                result.Number.ShouldEqual(query.Number);
            }
        }
    }
}
