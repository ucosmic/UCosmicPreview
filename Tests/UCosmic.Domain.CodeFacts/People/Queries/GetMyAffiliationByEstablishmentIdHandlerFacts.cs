using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class GetMyAffiliationByEstablishmentIdHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenQueryArgIsNull()
            {
                var handler = new GetMyAffiliationByEstablishmentIdHandler(null);
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
                var query = new GetMyAffiliationByEstablishmentIdQuery
                {
                    Principal = principal,
                };
                Expression<Func<GetUserByNameQuery, bool>> userByNameQuery = q =>
                    q.Name == query.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(p => p.Execute(It.Is(userByNameQuery)))
                    .Returns(null as User);
                var handler = new GetMyAffiliationByEstablishmentIdHandler(queryProcessor.Object);

                handler.Handle(query);

                queryProcessor.Verify(m => m.Execute(It.Is(userByNameQuery)), Times.Once());
            }

            [TestMethod]
            public void ReturnsNull_WhenQueryForUser_ReturnsNull()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var query = new GetMyAffiliationByEstablishmentIdQuery
                {
                    Principal = principal,
                };
                Expression<Func<GetUserByNameQuery, bool>> userByNameQuery = q =>
                    q.Name == query.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(p => p.Execute(It.Is(userByNameQuery)))
                    .Returns(null as User);
                var handler = new GetMyAffiliationByEstablishmentIdHandler(queryProcessor.Object);

                var result = handler.Handle(query);

                result.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsNull_WhenUserPerson_DoesNotHaveMatchingAffiliation()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var query = new GetMyAffiliationByEstablishmentIdQuery
                {
                    Principal = principal,
                    EstablishmentId = 1,
                };
                Expression<Func<GetUserByNameQuery, bool>> userByNameQuery = q =>
                    q.Name == query.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(p => p.Execute(It.Is(userByNameQuery)))
                    .Returns(new User
                    {
                        Person = new Person
                        {
                            Affiliations = new[]
                            {
                                new Affiliation  { EstablishmentId = 2 },
                                new Affiliation  { EstablishmentId = 3 },
                                new Affiliation  { EstablishmentId = 4 },
                            }
                        }
                    });
                var handler = new GetMyAffiliationByEstablishmentIdHandler(queryProcessor.Object);

                var result = handler.Handle(query);

                result.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsEmailAddress_WhenMatchingUserAndNumber_IsFound()
            {
                const string principalIdentityName = "user@domain.tld";
                var principal = principalIdentityName.AsPrincipal();
                var query = new GetMyAffiliationByEstablishmentIdQuery
                {
                    Principal = principal,
                    EstablishmentId = 1,
                };
                Expression<Func<GetUserByNameQuery, bool>> userByNameQuery = q =>
                    q.Name == query.Principal.Identity.Name;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(p => p.Execute(It.Is(userByNameQuery)))
                    .Returns(new User
                    {
                        Person = new Person
                        {
                            Affiliations = new[]
                            {
                                new Affiliation  { EstablishmentId = 1 },
                                new Affiliation  { EstablishmentId = 3 },
                                new Affiliation  { EstablishmentId = 4 },
                            }
                        }
                    });
                var handler = new GetMyAffiliationByEstablishmentIdHandler(queryProcessor.Object);

                var result = handler.Handle(query);

                result.ShouldNotBeNull();
                result.EstablishmentId.ShouldEqual(query.EstablishmentId);
            }
        }
    }
}
