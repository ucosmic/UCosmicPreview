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
    public class FindPeopleWithLastNameHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenQueryArgIsNull()
            {
                var handler = new FindPeopleWithLastNameHandler(null);
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
                var handler = new FindPeopleWithLastNameHandler(null);
                ValidationException exception = null;
                try
                {
                    handler.Handle(new FindPeopleWithLastNameQuery());
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
                var handler = new FindPeopleWithLastNameHandler(null);
                ValidationException exception = null;
                try
                {
                    handler.Handle(new FindPeopleWithLastNameQuery
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
                var handler = new FindPeopleWithLastNameHandler(null);
                ValidationException exception = null;
                try
                {
                    handler.Handle(new FindPeopleWithLastNameQuery
                    {
                        Term = "\r"
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
                exceptionError.AttemptedValue.ShouldEqual("\r");
            }

            [TestMethod]
            public void QueriesPeople_WithEagerLoading()
            {
                var query = new FindPeopleWithLastNameQuery
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
                entities.Setup(p => p.Get<Person>()).Returns(fakes);
                entities.Setup(m => m.EagerLoad(fakes,
                    It.IsAny<Expression<Func<Person, object>>>())).Returns(fakes);
                var handler = new FindPeopleWithLastNameHandler(entities.Object);

                handler.Handle(query);

                entities.Verify(p => p.Get<Person>(), Times.Once());
                entities.Verify(m => m.EagerLoad(fakes,
                    It.IsAny<Expression<Func<Person, object>>>()),
                        Times.Exactly(query.EagerLoad.Count()));
            }

            [TestMethod]
            public void ReturnsOrderedResults()
            {
                var query = new FindPeopleWithLastNameQuery
                {
                    Term = "t",
                    TermMatchStrategy = StringMatchStrategy.Contains,
                    OrderBy = new Dictionary<Expression<Func<Person, object>>, OrderByDirection>
                    {
                        { p => p.LastName, OrderByDirection.Descending },
                        { p => p.FirstName, OrderByDirection.Ascending },
                    }
                };
                var fakes = FakePeople();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Get<Person>()).Returns(fakes);
                entities.Setup(m => m.EagerLoad(fakes,
                    It.IsAny<Expression<Func<Person, object>>>())).Returns(fakes);
                var handler = new FindPeopleWithLastNameHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Count().ShouldEqual(4);
                results.Skip(0).First().LastName.ShouldEqual(fakes.Skip(3).First().LastName);
                results.Skip(1).First().LastName.ShouldEqual(fakes.Skip(2).First().LastName);
                results.Skip(2).First().LastName.ShouldEqual(fakes.Skip(1).First().LastName);
                results.Skip(3).First().LastName.ShouldEqual(fakes.Skip(0).First().LastName);
            }

            [TestMethod]
            public void FindsPeopleWithLastName_StartingWithTerm()
            {
                var query = new FindPeopleWithLastNameQuery
                {
                    Term = "wes",
                    TermMatchStrategy = StringMatchStrategy.StartsWith,
                };
                var fakes = FakePeople();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Get<Person>()).Returns(fakes);
                entities.Setup(m => m.EagerLoad(fakes,
                    It.IsAny<Expression<Func<Person, object>>>())).Returns(fakes);
                var handler = new FindPeopleWithLastNameHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Count().ShouldEqual(2);
                results.ToList().ForEach(i => i.LastName.ShouldEqual("West"));
            }

            [TestMethod]
            public void FindsPeopleWithLastName_ContainingTerm()
            {
                var query = new FindPeopleWithLastNameQuery
                {
                    Term = "st",
                    TermMatchStrategy = StringMatchStrategy.Contains,
                };
                var fakes = FakePeople();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Get<Person>()).Returns(fakes);
                entities.Setup(m => m.EagerLoad(fakes,
                    It.IsAny<Expression<Func<Person, object>>>())).Returns(fakes);
                var handler = new FindPeopleWithLastNameHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Count().ShouldEqual(2);
                results.ToList().ForEach(i => new[]
                {
                    fakes.ToArray()[2].LastName, 
                    fakes.ToArray()[3].LastName, 
                }.ShouldContain(i.LastName));
            }

            [TestMethod]
            public void DoesNotFindPeopleWithLastName_NotStartingWithTerm()
            {
                var query = new FindPeopleWithLastNameQuery
                {
                    Term = "nor",
                    TermMatchStrategy = StringMatchStrategy.StartsWith,
                };
                var fakes = FakePeople();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Get<Person>()).Returns(fakes);
                entities.Setup(m => m.EagerLoad(fakes,
                    It.IsAny<Expression<Func<Person, object>>>())).Returns(fakes);
                var handler = new FindPeopleWithLastNameHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Count().ShouldEqual(2);
                results.ToList().ForEach(i => i.LastName.ShouldNotEqual("West"));
            }

            [TestMethod]
            public void DoesNotFindPeopleWithLastName_NotContainingTerm()
            {
                var query = new FindPeopleWithLastNameQuery
                {
                    Term = "th",
                    TermMatchStrategy = StringMatchStrategy.Contains,
                };
                var fakes = FakePeople();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Get<Person>()).Returns(fakes);
                entities.Setup(m => m.EagerLoad(fakes,
                    It.IsAny<Expression<Func<Person, object>>>())).Returns(fakes);
                var handler = new FindPeopleWithLastNameHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Count().ShouldEqual(2);
                results.ToList().ForEach(i => i.LastName.ShouldNotEqual("West"));
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
