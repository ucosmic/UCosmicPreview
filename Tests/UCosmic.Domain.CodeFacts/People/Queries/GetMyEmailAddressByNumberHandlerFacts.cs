using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class GetMyEmailAddressByNumberHandlerFacts
    // ReSharper restore UnusedMember.Global
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
                Expression<Func<GetUserByNameQuery, bool>> userByNameQuery = q =>
                    q.Name == query.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(p => p.Execute(It.Is(userByNameQuery)))
                    .Returns(null as User);
                var handler = new GetMyEmailAddressByNumberHandler(queryProcessor.Object);

                handler.Handle(query);

                queryProcessor.Verify(m => m.Execute(It.Is(userByNameQuery)), Times.Once());
            }

            [TestMethod]
            public void ThrowsNullReferenceException_WhenPrincipalIsNull()
            {
                var query = new GetMyEmailAddressByNumberQuery
                {
                    Principal = null,
                };
                Expression<Func<GetUserByNameQuery, bool>> userByNameQuery = q =>
                    q.Name == query.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(p => p.Execute(It.Is(userByNameQuery)))
                    .Returns(null as User);
                var handler = new GetMyEmailAddressByNumberHandler(queryProcessor.Object);
                NullReferenceException exception = null;

                try
                {
                    handler.Handle(query);
                }
                catch (NullReferenceException ex)
                {
                    exception = ex;
                }

                queryProcessor.Verify(m => m.Execute(It.Is(userByNameQuery)), Times.Never());
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
                Expression<Func<GetUserByNameQuery, bool>> userByNameQuery = q =>
                    q.Name == query.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(p => p.Execute(It.Is(userByNameQuery)))
                    .Returns(null as User);
                var handler = new GetMyEmailAddressByNumberHandler(queryProcessor.Object);

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
                Expression<Func<GetUserByNameQuery, bool>> userByNameQuery = q =>
                    q.Name == query.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(p => p.Execute(It.Is(userByNameQuery)))
                    .Returns(new User
                    {
                        Person = new Person
                        {
                            Emails = new[]
                            {
                                new EmailAddress { Number = 2 },
                                new EmailAddress { Number = 3 },
                                new EmailAddress { Number = 4 },
                            }
                        }
                    });
                var handler = new GetMyEmailAddressByNumberHandler(queryProcessor.Object);

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
                Expression<Func<GetUserByNameQuery, bool>> userByNameQuery = q =>
                    q.Name == query.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(p => p.Execute(It.Is(userByNameQuery)))
                    .Returns(new User
                    {
                        Person = new Person
                        {
                            Emails = new[]
                            {
                                new EmailAddress { Number = 1 },
                                new EmailAddress { Number = 3 },
                                new EmailAddress { Number = 4 },
                            }
                        }
                    });
                var handler = new GetMyEmailAddressByNumberHandler(queryProcessor.Object);

                var result = handler.Handle(query);

                result.ShouldNotBeNull();
                result.Number.ShouldEqual(query.Number);
            }
        }
    }
}
