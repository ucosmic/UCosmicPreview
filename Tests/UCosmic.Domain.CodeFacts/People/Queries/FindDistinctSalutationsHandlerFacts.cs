using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using System;
using Moq;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class FindDistinctSalutationsHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenQueryArgIsNull()
            {
                var handler = new FindDistinctSalutationsHandler(null);
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
            public void DoesNotReturn_NullSalutations()
            {
                var query = new FindDistinctSalutationsQuery();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Query<Person>()).Returns(new[]
                {
                    new Person{ Salutation = null },
                    new Person{ Salutation = "H.R.H." },
                }.AsQueryable);
                var handler = new FindDistinctSalutationsHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Length.ShouldEqual(1);
                results.Any(s => s == null).ShouldBeFalse();
            }

            [TestMethod]
            public void DoesNotReturn_EmptyStringSalutations()
            {
                var query = new FindDistinctSalutationsQuery();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Query<Person>()).Returns(new[]
                {
                    new Person{ Salutation = string.Empty },
                    new Person{ Salutation = "H.R.H." },
                }.AsQueryable);
                var handler = new FindDistinctSalutationsHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Length.ShouldEqual(1);
                results.Any(s => s == string.Empty).ShouldBeFalse();
            }

            [TestMethod]
            public void DoesNotReturn_WhiteSpaceSalutations()
            {
                var query = new FindDistinctSalutationsQuery();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Query<Person>()).Returns(new[]
                {
                    new Person{ Salutation = "\r " },
                    new Person{ Salutation = "H.R.H." },
                }.AsQueryable);
                var handler = new FindDistinctSalutationsHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Length.ShouldEqual(1);
                results.Any(s => s == "\r ").ShouldBeFalse();
            }

            [TestMethod]
            public void DoesNotReturn_DuplicateSalutations()
            {
                var query = new FindDistinctSalutationsQuery();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Query<Person>()).Returns(new[]
                {
                    new Person{ Salutation = "H.R.H." },
                    new Person{ Salutation = "H.R.H." },
                    new Person{ Salutation = "H.R.H." },
                }.AsQueryable);
                var handler = new FindDistinctSalutationsHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Length.ShouldEqual(1);
                results.Count(s => s == "H.R.H.").ShouldEqual(1);
            }

            [TestMethod]
            public void DoesNotReturn_ExcludedSalutations()
            {
                var query = new FindDistinctSalutationsQuery
                {
                    Exclude = new[]
                    {
                        "Dr.",
                        "Mr."
                    }
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Query<Person>()).Returns(new[]
                {
                    new Person{ Salutation = "Dr." },
                    new Person{ Salutation = "Mr." },
                    new Person{ Salutation = "H.R.H." },
                }.AsQueryable);
                var handler = new FindDistinctSalutationsHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Length.ShouldEqual(1);
                results.Count(s => s == "Dr.").ShouldEqual(0);
                results.Count(s => s == "Mr.").ShouldEqual(0);
                results.Count(s => s == "H.R.H.").ShouldEqual(1);
            }
        }
    }
}
