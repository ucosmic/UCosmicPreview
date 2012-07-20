using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using System;
using Moq;

namespace UCosmic.Domain.Establishments
{
    // ReSharper disable UnusedMember.Global
    public class GetEstablishmentByIdHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenQueryArgIsNull()
            {
                var handler = new GetEstablishmentByIdHandler(null);
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
            public void ReturnsNull_WhenIdCannotBeMatched()
            {
                const int id = 9;
                var query = new GetEstablishmentByIdQuery(id);
                var establishments = new Establishment[] {}.AsQueryable();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Get<Establishment>()).Returns(establishments);
                var handler = new GetEstablishmentByIdHandler(entities.Object);

                var result = handler.Handle(query);

                result.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsEstablishment_WhenIdCanBeMatched()
            {
                const int id = 6;
                var query = new GetEstablishmentByIdQuery(id);
                var establishment = new Establishment
                {
                    RevisionId = id,
                };
                var establishments = new[]
                {
                    establishment,
                }.AsQueryable();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Get<Establishment>()).Returns(establishments);
                var handler = new GetEstablishmentByIdHandler(entities.Object);

                var result = handler.Handle(query);

                result.ShouldNotBeNull();
                result.RevisionId.ShouldEqual(id);
            }
        }
    }
}
