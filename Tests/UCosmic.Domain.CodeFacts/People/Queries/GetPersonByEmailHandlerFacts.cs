using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using System;
using Moq;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class GetPersonByEmailHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenQueryArgIsNull()
            {
                var handler = new GetPersonByEmailHandler(null);
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
            public void ReturnsNull_WhenEmailCannotBeMatched()
            {
                var query = new GetPersonByEmailQuery();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Get<Person>()).Returns(new Person[] { }.AsQueryable);
                var handler = new GetPersonByEmailHandler(entities.Object);

                var result = handler.Handle(query);

                result.ShouldBeNull();
            }

            [TestMethod]
            public void ReturnsPerson_WhenEmailCanBeMatched()
            {
                const string email = "user@domain.tld";
                var query = new GetPersonByEmailQuery
                {
                    Email = email,
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Get<Person>()).Returns(new[]
                {
                    new Person
                    {
                        Emails = new[]
                        {
                            new EmailAddress { Value = email }
                        }
                    },
                }.AsQueryable);
                var handler = new GetPersonByEmailHandler(entities.Object);

                var result = handler.Handle(query);

                result.ShouldNotBeNull();
                result.Emails.Count.ShouldEqual(1);
                result.Emails.Single().Value.ShouldEqual(email);
            }
        }
    }
}
