using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using System;
using Moq;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class FindDistinctSuffixesHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenQueryArgIsNull()
            {
                var handler = new FindDistinctSuffixesHandler(null);
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
            public void DoesNotReturn_NullSuffixes()
            {
                var query = new FindDistinctSuffixesQuery();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Query<Person>()).Returns(new[]
                {
                    new Person{ Suffix = null },
                    new Person{ Suffix = "S1" },
                }.AsQueryable);
                var handler = new FindDistinctSuffixesHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Length.ShouldEqual(1);
                results.Any(s => s == null).ShouldBeFalse();
            }

            [TestMethod]
            public void DoesNotReturn_EmptyStringSuffixes()
            {
                var query = new FindDistinctSuffixesQuery();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Query<Person>()).Returns(new[]
                {
                    new Person{ Suffix = string.Empty },
                    new Person{ Suffix = "S1" },
                }.AsQueryable);
                var handler = new FindDistinctSuffixesHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Length.ShouldEqual(1);
                results.Any(s => s == string.Empty).ShouldBeFalse();
            }

            [TestMethod]
            public void DoesNotReturn_WhiteSpaceSuffixes()
            {
                var query = new FindDistinctSuffixesQuery();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Query<Person>()).Returns(new[]
                {
                    new Person{ Suffix = "\r " },
                    new Person{ Suffix = "S1" },
                }.AsQueryable);
                var handler = new FindDistinctSuffixesHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Length.ShouldEqual(1);
                results.Any(s => s == "\r ").ShouldBeFalse();
            }

            [TestMethod]
            public void DoesNotReturn_DuplicateSuffixes()
            {
                var query = new FindDistinctSuffixesQuery();
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Query<Person>()).Returns(new[]
                {
                    new Person{ Suffix = "S1" },
                    new Person{ Suffix = "S1" },
                    new Person{ Suffix = "S1" },
                }.AsQueryable);
                var handler = new FindDistinctSuffixesHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Length.ShouldEqual(1);
                results.Count(s => s == "S1").ShouldEqual(1);
            }

            [TestMethod]
            public void DoesNotReturn_ExcludedSuffixes()
            {
                var query = new FindDistinctSuffixesQuery
                {
                    Exclude = new[]
                    {
                        "S1",
                        "S2"
                    }
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict);
                entities.Setup(p => p.Query<Person>()).Returns(new[]
                {
                    new Person{ Suffix = "S1" },
                    new Person{ Suffix = "S2" },
                    new Person{ Suffix = "H.R.H." },
                }.AsQueryable);
                var handler = new FindDistinctSuffixesHandler(entities.Object);

                var results = handler.Handle(query);

                results.ShouldNotBeNull();
                results.Length.ShouldEqual(1);
                results.Count(s => s == "S1").ShouldEqual(0);
                results.Count(s => s == "S2").ShouldEqual(0);
                results.Count(s => s == "H.R.H.").ShouldEqual(1);
            }
        }
    }
}
