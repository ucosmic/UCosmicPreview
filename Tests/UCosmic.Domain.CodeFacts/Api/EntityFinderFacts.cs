using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace UCosmic.Domain
{
    // ReSharper disable UnusedMember.Global
    public class EntityFinderFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class FindMany_Method
        {
            [TestMethod]
            public void IsPublicAbstract()
            {
                // arrange
                var mockQueries = new Mock<IQueryEntities>();
                var mockFinder = new Mock<EntityFinder<Entity>>(mockQueries.Object);
                mockFinder.Setup(m => m.FindMany(It.IsAny<EntityQueryCriteria<Entity>>()))
                    .Returns(new[] { new Mock<Entity>().Object, new Mock<Entity>().Object });

                // act
                var finder = mockFinder.Object;
                var results = finder.FindMany(null);

                // assert
                results.ShouldNotBeNull();
                results.Count.ShouldEqual(2);
            }
        }

        [TestClass]
        public class FindOne_Method
        {
            [TestMethod]
            public void IsPublicAbstract()
            {
                // arrange
                var mockQueries = new Mock<IQueryEntities>();
                var mockFinder = new Mock<EntityFinder<Entity>>(mockQueries.Object);
                mockFinder.Setup(m => m.FindOne(It.IsAny<EntityQueryCriteria<Entity>>()))
                    .Returns(new Mock<Entity>().Object);

                // act
                var finder = mockFinder.Object;
                var result = finder.FindOne(null);

                // assert
                result.ShouldNotBeNull();
            }
        }
    }
}
