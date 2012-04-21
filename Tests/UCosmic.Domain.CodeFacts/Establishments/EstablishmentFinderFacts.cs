using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace UCosmic.Domain.Establishments
{
    // ReSharper disable UnusedMember.Global
    public class EstablishmentFinderFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class FindManyMethod
        {
            [TestMethod]
            public void Returns2TestEstablishments_WithDefaultCriteria()
            {
                // arrange
                var data = new[] { new Establishment(), new Establishment(), }.AsQueryable();
                var entityQueries = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entityQueries.Setup(i => i.Establishments).Returns(data);
                var finder = new EstablishmentFinder(entityQueries.Object);

                // act
                var results = finder.FindMany(With<Establishment>.DefaultCriteria()).ToList();

                // assert
                results.ShouldNotBeNull();
                results.Count().ShouldEqual(2);
            }
        }
    }
}
