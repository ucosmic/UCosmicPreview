using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using System;
using Moq;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class FindPeopleWithFirstNameHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenQueryArgIsNull()
            {
                var handler = new FindPeopleWithFirstNameHandler(null);
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
            public void ThrowsValidationException_WhenQueryTermIsNull()
            {
                var handler = new FindPeopleWithFirstNameHandler(null);
                ValidationException exception = null;
                try
                {
                    handler.Handle(new FindPeopleWithFirstNameQuery());
                }
                catch (ValidationException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.Errors.ShouldNotBeNull();
                // ReSharper restore PossibleNullReferenceException
                exception.Errors.Count().ShouldEqual(1);
                var exceptionError = exception.Errors.Single();
                exceptionError.PropertyName.ShouldEqual("Term");
                exceptionError.ErrorMessage.ShouldEqual("Term cannot be null or white space string");
                exceptionError.AttemptedValue.ShouldBeNull();
            }

            [TestMethod]
            public void ThrowsValidationException_WhenQueryTermIsEmptyString()
            {
                var handler = new FindPeopleWithFirstNameHandler(null);
                ValidationException exception = null;
                try
                {
                    handler.Handle(new FindPeopleWithFirstNameQuery
                    {
                        Term = string.Empty
                    });
                }
                catch (ValidationException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.Errors.ShouldNotBeNull();
                // ReSharper restore PossibleNullReferenceException
                exception.Errors.Count().ShouldEqual(1);
                var exceptionError = exception.Errors.Single();
                exceptionError.PropertyName.ShouldEqual("Term");
                exceptionError.ErrorMessage.ShouldEqual("Term cannot be null or white space string");
                exceptionError.AttemptedValue.ShouldEqual(string.Empty);
            }

            [TestMethod]
            public void ThrowsValidationException_WhenQueryTermIsWhiteSpace()
            {
                var handler = new FindPeopleWithFirstNameHandler(null);
                ValidationException exception = null;
                try
                {
                    handler.Handle(new FindPeopleWithFirstNameQuery
                    {
                        Term = "\t"
                    });
                }
                catch (ValidationException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.Errors.ShouldNotBeNull();
                // ReSharper restore PossibleNullReferenceException
                exception.Errors.Count().ShouldEqual(1);
                var exceptionError = exception.Errors.Single();
                exceptionError.PropertyName.ShouldEqual("Term");
                exceptionError.ErrorMessage.ShouldEqual("Term cannot be null or white space string");
                exceptionError.AttemptedValue.ShouldEqual("\t");
            }

            [TestMethod]
            public void QueriesPeople_WithEagerLoading()
            {
                var query = new FindPeopleWithFirstNameQuery
                {
                    Term = "test",
                    EagerLoad = new Expression<Func<Person, object>>[]
                    {
                        p => p.Emails.Select(e => e.Confirmations),
                        p => p.Messages,
                    },
                };
                var fakes = FakePeople();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Query<Person>()).Returns(fakes);
                entities.Setup(m => m.EagerLoad(fakes,
                    It.IsAny<Expression<Func<Person, object>>>())).Returns(fakes);
                var handler = new FindPeopleWithFirstNameHandler(entities.Object);

                handler.Handle(query);

                entities.Verify(p => p.Query<Person>(), Times.Once());
                entities.Verify(m => m.EagerLoad(fakes,
                    It.IsAny<Expression<Func<Person, object>>>()),
                        Times.Exactly(query.EagerLoad.Count()));
            }

            [TestMethod]
            public void ReturnsOrderedResults()
            {
                var query = new FindPeopleWithFirstNameQuery
                {
                    Term = "a",
                    TermMatchStrategy = StringMatchStrategy.Contains,
                    OrderBy = new Dictionary<Expression<Func<Person, object>>, OrderByDirection>
                    {
                        { p => p.LastName, OrderByDirection.Descending },
                        { p => p.FirstName, OrderByDirection.Ascending },
                    }
                };
                var fakes = FakePeople();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Query<Person>()).Returns(fakes);
                entities.Setup(m => m.EagerLoad(fakes,
                    It.IsAny<Expression<Func<Person, object>>>())).Returns(fakes);
                var handler = new FindPeopleWithFirstNameHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Count().ShouldEqual(4);
                results.Skip(0).First().FirstName.ShouldEqual(fakes.Skip(3).First().FirstName);
                results.Skip(1).First().FirstName.ShouldEqual(fakes.Skip(2).First().FirstName);
                results.Skip(2).First().FirstName.ShouldEqual(fakes.Skip(1).First().FirstName);
                results.Skip(3).First().FirstName.ShouldEqual(fakes.Skip(0).First().FirstName);
            }

            [TestMethod]
            public void FindsPeopleWithFirstName_StartingWithTerm()
            {
                var query = new FindPeopleWithFirstNameQuery
                {
                    Term = "daniel",
                    TermMatchStrategy = StringMatchStrategy.StartsWith,
                };
                var fakes = FakePeople();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Query<Person>()).Returns(fakes);
                entities.Setup(m => m.EagerLoad(fakes,
                    It.IsAny<Expression<Func<Person, object>>>())).Returns(fakes);
                var handler = new FindPeopleWithFirstNameHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Count().ShouldEqual(2);
                results.ToList().ForEach(i => i.FirstName.ShouldStartWith("Daniel"));
            }

            [TestMethod]
            public void FindsPeopleWithFirstName_ContainingTerm()
            {
                var query = new FindPeopleWithFirstNameQuery
                {
                    Term = "am",
                    TermMatchStrategy = StringMatchStrategy.Contains,
                };
                var fakes = FakePeople();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Query<Person>()).Returns(fakes);
                entities.Setup(m => m.EagerLoad(fakes,
                    It.IsAny<Expression<Func<Person, object>>>())).Returns(fakes);
                var handler = new FindPeopleWithFirstNameHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Count().ShouldEqual(2);
                results.ToList().ForEach(i => new[]
                {
                    fakes.First().FirstName,
                    fakes.Last().FirstName
                }.ShouldContain(i.FirstName));
            }

            [TestMethod]
            public void DoesNotFindPeopleWithFirstName_NotStartingWithTerm()
            {
                var query = new FindPeopleWithFirstNameQuery
                {
                    Term = "Daniel",
                    TermMatchStrategy = StringMatchStrategy.StartsWith,
                };
                var fakes = FakePeople();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Query<Person>()).Returns(fakes);
                entities.Setup(m => m.EagerLoad(fakes,
                    It.IsAny<Expression<Func<Person, object>>>())).Returns(fakes);
                var handler = new FindPeopleWithFirstNameHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Count().ShouldEqual(2);
                results.ToList().ForEach(i => new[]
                {
                    fakes.First().FirstName,
                    fakes.Last().FirstName
                }.ShouldNotContain(i.FirstName));
}

            [TestMethod]
            public void DoesNotFindPeopleWithFirstName_NotContainingTerm()
            {
                var query = new FindPeopleWithFirstNameQuery
                {
                    Term = "am",
                    TermMatchStrategy = StringMatchStrategy.Contains,
                };
                var fakes = FakePeople();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Query<Person>()).Returns(fakes);
                entities.Setup(m => m.EagerLoad(fakes,
                    It.IsAny<Expression<Func<Person, object>>>())).Returns(fakes);
                var handler = new FindPeopleWithFirstNameHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Count().ShouldEqual(2);
                results.ToList().ForEach(i => new[]
                {
                    fakes.ToArray()[1].FirstName,
                    fakes.ToArray()[2].FirstName
                }.ShouldNotContain(i.FirstName));
            }

            private static IQueryable<Person> FakePeople()
            {
                var queryable = new[]
                {
                    new Person
                    {
                        LastName = "North",
                        FirstName = "William",
                        Emails = new[] { new EmailAddress { Value = "sample1@domain.tld" }, },
                    },
                    new Person
                    {
                        LastName = "North",
                        FirstName = "Daniel",
                        Emails = new[] { new EmailAddress { Value = "sample2@domain.tld" }, },
                    },
                    new Person
                    {
                        LastName = "West",
                        FirstName = "Danielle",
                        Emails = new[] { new EmailAddress { Value = "test2@site.tld"} , },
                    },
                    new Person
                    {
                        LastName = "West",
                        FirstName = "Adam",
                        Emails = new[] { new EmailAddress { Value = "test1@site.tld"} , },
                    },
                }.AsQueryable();
                return queryable;
            }
        }
    }
}
