using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Email;
using UCosmic.Domain.Identity;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Domain.Languages;
using UCosmic.Domain.People;
using UCosmic.Domain.Places;

namespace UCosmic.Domain
{
    // ReSharper disable UnusedMember.Global
    public class InterfaceQueryEntitiesFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class IQueryableProperties
        {
            [TestMethod]
            public void Languages_HasGet()
            {
                var mockQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                mockQueries.Setup(p => p.Languages).Returns(null as IQueryable<Language>);
                var queries = mockQueries.Object;
                queries.ShouldNotBeNull();
                queries.Languages.ShouldBeNull();
            }

            [TestMethod]
            public void Places_HasGet()
            {
                var mockQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                mockQueries.Setup(p => p.Places).Returns(null as IQueryable<Place>);
                var queries = mockQueries.Object;
                queries.ShouldNotBeNull();
                queries.Places.ShouldBeNull();
            }

            [TestMethod]
            public void GeoNamesToponyms_HasGet()
            {
                var mockQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                mockQueries.Setup(p => p.GeoNamesToponyms)
                    .Returns(null as IQueryable<GeoNamesToponym>);
                var queries = mockQueries.Object;
                queries.ShouldNotBeNull();
                queries.GeoNamesToponyms.ShouldBeNull();
            }

            [TestMethod]
            public void GeoPlanetPlaces_HasGet()
            {
                var mockQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                mockQueries.Setup(p => p.GeoPlanetPlaces)
                    .Returns(null as IQueryable<GeoPlanetPlace>);
                var queries = mockQueries.Object;
                queries.ShouldNotBeNull();
                queries.GeoPlanetPlaces.ShouldBeNull();
            }

            [TestMethod]
            public void Users_HasGet()
            {
                var mockQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                mockQueries.Setup(p => p.Users).Returns(null as IQueryable<User>);
                var queries = mockQueries.Object;
                queries.ShouldNotBeNull();
                queries.Users.ShouldBeNull();
            }

            [TestMethod]
            public void Roles_HasGet()
            {
                var mockQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                mockQueries.Setup(p => p.Roles).Returns(null as IQueryable<Role>);
                var queries = mockQueries.Object;
                queries.ShouldNotBeNull();
                queries.Roles.ShouldBeNull();
            }

            [TestMethod]
            public void People_HasGet()
            {
                var mockQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                mockQueries.Setup(p => p.People).Returns(null as IQueryable<Person>);
                var queries = mockQueries.Object;
                queries.ShouldNotBeNull();
                queries.People.ShouldBeNull();
            }

            [TestMethod]
            public void EmailTemplates_HasGet()
            {
                var mockQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                mockQueries.Setup(p => p.EmailTemplates)
                    .Returns(null as IQueryable<EmailTemplate>);
                var queries = mockQueries.Object;
                queries.ShouldNotBeNull();
                queries.EmailTemplates.ShouldBeNull();
            }

            [TestMethod]
            public void InstitutionalAgreements_HasGet()
            {
                var mockQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                mockQueries.Setup(p => p.InstitutionalAgreements)
                    .Returns(null as IQueryable<InstitutionalAgreement>);
                var queries = mockQueries.Object;
                queries.ShouldNotBeNull();
                queries.InstitutionalAgreements.ShouldBeNull();
            }

            [TestMethod]
            public void InstitutionalAgreementConfigurations_HasGet()
            {
                var mockQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                mockQueries.Setup(p => p.InstitutionalAgreementConfigurations)
                    .Returns(null as IQueryable<InstitutionalAgreementConfiguration>);
                var queries = mockQueries.Object;
                queries.ShouldNotBeNull();
                queries.InstitutionalAgreementConfigurations.ShouldBeNull();
            }
        }

        [TestClass]
        public class ApplyInsertOrUpdateMethod
        {
            [TestMethod]
            public void ReturnsIQueryableGeneric()
            {
                var genericIQueryable = new List<Entity>().AsQueryable();

                var mockQueries = new Mock<IQueryEntities>(MockBehavior.Strict);
                mockQueries
                    .Setup(p => p.ApplyInsertOrUpdate(
                        It.IsAny<IQueryable<Entity>>(),
                        It.IsAny<EntityQueryCriteria<Entity>>()))
                    .Returns(genericIQueryable);
                var queries = mockQueries.Object;

                var resultIQueryable = queries.ApplyInsertOrUpdate(genericIQueryable, null);
                resultIQueryable.ShouldEqual(genericIQueryable);
            }
        }

        [TestClass]
        public class ApplyEagerLoadingMethod
        {
            [TestMethod]
            public void ReturnsIQueryableGeneric()
            {
                var genericIQueryable = new List<Entity>().AsQueryable();

                var mockQueries = new Mock<IQueryEntities>(MockBehavior.Default);
                mockQueries
                    .Setup(p => p.ApplyEagerLoading(
                        It.IsAny<IQueryable<Entity>>(),
                        It.IsAny<EntityQueryCriteria<Entity>>()))
                    .Returns(genericIQueryable);
                var queries = mockQueries.Object;

                var resultIQueryable = queries.ApplyInsertOrUpdate(genericIQueryable, null);
                resultIQueryable.ShouldEqual(genericIQueryable);
            }
        }
    }
}
