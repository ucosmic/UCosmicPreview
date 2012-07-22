using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using System;
using Moq;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class GetPersonByGuidHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenQueryArgIsNull()
            {
                var handler = new GetPersonByGuidHandler(null);
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
            public void ReturnsNull_WhenGuidCannotBeMatched()
            {
                var guid = Guid.NewGuid();
                var query = new GetPersonByGuidQuery(guid);
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Read<Person>()).Returns(new Person[] { }.AsQueryable);
                var handler = new GetPersonByGuidHandler(entities.Object);

                var result = handler.Handle(query);

                result.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsPerson_WhenGuidCanBeMatched()
            {
                var guid = Guid.NewGuid();
                var query = new GetPersonByGuidQuery(guid);
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Read<Person>()).Returns(new[]
                {
                    new Person
                    {
                        EntityId = guid,
                    },
                }.AsQueryable);
                var handler = new GetPersonByGuidHandler(entities.Object);

                var result = handler.Handle(query);

                result.ShouldNotBeNull();
                result.EntityId.ShouldEqual(guid);
            }
        }
    }
}
