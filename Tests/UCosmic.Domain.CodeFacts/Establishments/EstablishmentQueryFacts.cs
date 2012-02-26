using System;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace UCosmic.Domain.Establishments
{
    // ReSharper disable UnusedMember.Global
    public class EstablishmentQueryFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class Constructor
        {
            [TestMethod]
            public void Defaults_HasParent_ToNull()
            {
                // arrange
                var mockQuery = new Mock<EstablishmentQuery>();

                // act
                var query = mockQuery.Object;

                // assert
                query.ShouldNotBeNull();
                query.HasParent.HasValue.ShouldBeFalse();
            }

            [TestMethod]
            public void Defaults_HasChildren_ToNull()
            {
                // arrange
                var mockQuery = new Mock<EstablishmentQuery>();

                // act
                var query = mockQuery.Object;

                // assert
                query.ShouldNotBeNull();
                query.HasChildren.HasValue.ShouldBeFalse();
            }

            [TestMethod]
            public void Defaults_EmailDomain_ToNull()
            {
                // arrange
                var mockQuery = new Mock<EstablishmentQuery>();

                // act
                var query = mockQuery.Object;

                // assert
                query.ShouldNotBeNull();
                query.EmailDomain.ShouldBeNull();
            }

            [TestMethod]
            public void Defaults_WebsiteUrl_ToNull()
            {
                // arrange
                var mockQuery = new Mock<EstablishmentQuery>();

                // act
                var query = mockQuery.Object;

                // assert
                query.ShouldNotBeNull();
                query.WebsiteUrl.ShouldBeNull();
            }

            [TestMethod]
            public void Defaults_AutoCompleteTerm_ToNull()
            {
                // arrange
                var mockQuery = new Mock<EstablishmentQuery>();

                // act
                var query = mockQuery.Object;

                // assert
                query.ShouldNotBeNull();
                query.AutoCompleteTerm.ShouldBeNull();
            }

            [TestMethod]
            public void Defaults_SamlEntityId_ToNull()
            {
                // arrange
                var mockQuery = new Mock<EstablishmentQuery>();

                // act
                var query = mockQuery.Object;

                // assert
                query.ShouldNotBeNull();
                query.SamlEntityId.ShouldBeNull();
            }
        }

        [TestClass]
        public class EstablishmentByStatic
        {
            [TestMethod]
            public void RevisionId_CreatesQuery()
            {
                // arrange
                const int revisionId = 6;

                // act
                var query = By<Establishment>.RevisionId(revisionId);


                // assert
                query.ShouldNotBeNull();
                query.RevisionId.ShouldEqual(revisionId);
            }

            [TestMethod]
            public void EntityId_CreatesQuery()
            {
                // arrange
                var entityId = Guid.NewGuid();

                // act
                var query = By<Establishment>.EntityId(entityId);


                // assert
                query.ShouldNotBeNull();
                query.EntityId.ShouldEqual(entityId);
            }

            [TestMethod]
            public void SamlEntityId_CreatesQuery()
            {
                // arrange
                const string samlEntityId = "https://www.site.com/Shibboleth.sso/blah/POST";

                // act
                var query = EstablishmentBy.SamlEntityId(samlEntityId);


                // assert
                query.ShouldNotBeNull();
                query.SamlEntityId.ShouldEqual(samlEntityId);
            }

            [TestMethod]
            public void EmailDomain_CreatesQuery()
            {
                // arrange
                const string emailDomain = "id@domain.tld";

                // act
                var query = EstablishmentBy.EmailDomain(emailDomain);


                // assert
                query.ShouldNotBeNull();
                query.EmailDomain.ShouldEqual(emailDomain);
            }

            [TestMethod]
            public void WebsiteUrl_CreatesQuery()
            {
                // arrange
                const string websiteUrl = "www.domain.tld";

                // act
                var query = EstablishmentBy.WebsiteUrl(websiteUrl);


                // assert
                query.ShouldNotBeNull();
                query.WebsiteUrl.ShouldEqual(websiteUrl);
            }
        }

        [TestClass]
        public class EstablishmentsWithStatic
        {
            [TestMethod]
            public void DefaultCriteria_CreatesQuery()
            {
                // arrange
                // act
                var query = With<Establishment>.DefaultCriteria();

                // assert
                query.ShouldNotBeNull();
            }

            [TestMethod]
            public void RevisionIds_CreatesQuery()
            {
                // arrange
                var revisionIds = new Collection<int> { 6, 12, 18 };

                // act
                var query = With<Establishment>.RevisionIds(revisionIds);

                // assert
                query.ShouldNotBeNull();
                query.RevisionIds.ShouldNotBeNull();
                query.RevisionIds.ShouldEqual(revisionIds);
            }

            [TestMethod]
            public void NoParentButWithChildren_CreatesQuery()
            {
                // arrange
                // act
                var query = EstablishmentsWith.NoParentButWithChildren();

                // assert
                query.ShouldNotBeNull();
                query.HasParent.HasValue.ShouldBeTrue();
                query.HasChildren.HasValue.ShouldBeTrue();
                // ReSharper disable PossibleInvalidOperationException
                query.HasParent.Value.ShouldBeFalse();
                query.HasChildren.Value.ShouldBeTrue();
                // ReSharper restore PossibleInvalidOperationException
            }

            [TestMethod]
            public void AutoCompleteTerm_With2Args_CreatesQuery()
            {
                // arrange
                const string autoCompleteTerm = "pro";

                // act
                var query = EstablishmentsWith.AutoCompleteTerm(autoCompleteTerm, new[] { 6, 3 });

                // assert
                query.ShouldNotBeNull();
                query.AutoCompleteTerm.ShouldEqual(autoCompleteTerm);
                query.MaxResults.HasValue.ShouldBeFalse();
                query.ExcludeRevisionIds.ShouldNotBeNull();
                query.ExcludeRevisionIds.Count.ShouldEqual(2);
            }

            [TestMethod]
            public void AutoCompleteTerm_With3Args_CreatesQuery()
            {
                // arrange
                const string autoCompleteTerm = "pro";
                const int maxResults = 10;
                var excludeRevisionIds = new Collection<int> { 6, 12, 19 };

                // act
                var query = EstablishmentsWith.AutoCompleteTerm(autoCompleteTerm, excludeRevisionIds, maxResults);

                // assert
                query.ShouldNotBeNull();
                query.AutoCompleteTerm.ShouldEqual(autoCompleteTerm);
                query.MaxResults.HasValue.ShouldBeTrue();
                // ReSharper disable PossibleInvalidOperationException
                query.MaxResults.Value.ShouldEqual(maxResults);
                // ReSharper restore PossibleInvalidOperationException
                query.ExcludeRevisionIds.ShouldNotBeNull();
                query.ExcludeRevisionIds.ShouldEqual(excludeRevisionIds);
            }
        }
    }
}
