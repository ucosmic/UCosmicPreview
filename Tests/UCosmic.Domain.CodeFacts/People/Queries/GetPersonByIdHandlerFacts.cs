using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using System;
using Moq;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class GetPersonByIdHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenQueryArgIsNull()
            {
                var handler = new GetPersonByIdHandler(null);
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
                var query = new GetPersonByIdQuery();
                var entities = new Mock<IQueryEntities>();
                entities.Setup(p => p.People).Returns(new Person[] { }.AsQueryable);
                var handler = new GetPersonByIdHandler(entities.Object);

                var result = handler.Handle(query);

                result.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsPerson_WhenIdCanBeMatched()
            {
                const int id = 6;
                var query = new GetPersonByIdQuery
                {
                    Id = id,
                };
                var entities = new Mock<IQueryEntities>();
                entities.Setup(p => p.People).Returns(new[]
                {
                    new Person
                    {
                        RevisionId = id,
                    },
                }.AsQueryable);
                var handler = new GetPersonByIdHandler(entities.Object);

                var result = handler.Handle(query);

                result.ShouldNotBeNull();
                result.RevisionId.ShouldEqual(id);
            }
        }
    }
}
