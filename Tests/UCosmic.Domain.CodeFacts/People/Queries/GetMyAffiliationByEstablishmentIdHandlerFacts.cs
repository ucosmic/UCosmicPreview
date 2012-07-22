using System;
using System.Linq;
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
                var affiliation = new Affiliation
                {
                    Person = new Person { User = new User { Name = principal.Identity.Name } }
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<Affiliation>()).Returns(new[] { affiliation }.AsQueryable);
                var handler = new GetMyAffiliationByEstablishmentIdHandler(entities.Object);

                handler.Handle(query);

                entities.Verify(m => m.Query<Affiliation>(), Times.Once());
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
                var affiliation = new Affiliation
                {
                    Person = new Person { User = new User { Name = "something else" } },
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<Affiliation>()).Returns(new[] { affiliation }.AsQueryable);
                var handler = new GetMyAffiliationByEstablishmentIdHandler(entities.Object);

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
                var affiliations = new[]
                {
                    new Affiliation
                    {
                        EstablishmentId = 2,
                        Person = new Person { User = new User { Name = principal.Identity.Name } }
                    },
                    new Affiliation
                    {
                        EstablishmentId = 3,
                        Person = new Person { User = new User { Name = principal.Identity.Name } }
                    },
                    new Affiliation
                    {
                        EstablishmentId = 4,
                        Person = new Person { User = new User { Name = principal.Identity.Name } }
                    },
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<Affiliation>()).Returns(affiliations.AsQueryable);
                var handler = new GetMyAffiliationByEstablishmentIdHandler(entities.Object);

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
                var affiliations = new[]
                {
                    new Affiliation
                    {
                        EstablishmentId = 1,
                        Person = new Person { User = new User { Name = principal.Identity.Name } }
                    },
                    new Affiliation
                    {
                        EstablishmentId = 3,
                        Person = new Person { User = new User { Name = principal.Identity.Name } }
                    },
                    new Affiliation
                    {
                        EstablishmentId = 4,
                        Person = new Person { User = new User { Name = principal.Identity.Name } }
                    },
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Query<Affiliation>()).Returns(affiliations.AsQueryable);
                var handler = new GetMyAffiliationByEstablishmentIdHandler(entities.Object);

                var result = handler.Handle(query);

                result.ShouldNotBeNull();
                result.EstablishmentId.ShouldEqual(query.EstablishmentId);
            }
        }
    }
}
