//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Should;

//namespace UCosmic.Domain
//{
//    // ReSharper disable UnusedMember.Global
//    public class RevisableEntityQueryCriteriaFacts
//    // ReSharper restore UnusedMember.Global
//    {
//        [TestClass]
//        public class Constructor
//        {
//            [TestMethod]
//            public void Defaults_IsCurrent_ToTrue()
//            {
//                // arrange
//                var mockQuery = new Mock<RevisableEntityQueryCriteria<RevisableEntity>>();

//                // act
//                var query = mockQuery.Object;

//                // assert
//                query.ShouldNotBeNull();
//                query.IsCurrent.HasValue.ShouldBeTrue();
//                // ReSharper disable PossibleInvalidOperationException
//                query.IsCurrent.Value.ShouldBeTrue();
//                // ReSharper restore PossibleInvalidOperationException
//            }

//            [TestMethod]
//            public void Defaults_IsArchived_ToFalse()
//            {
//                // arrange
//                var mockQuery = new Mock<RevisableEntityQueryCriteria<RevisableEntity>>();

//                // act
//                var query = mockQuery.Object;

//                // assert
//                query.ShouldNotBeNull();
//                query.IsArchived.HasValue.ShouldBeTrue();
//                // ReSharper disable PossibleInvalidOperationException
//                query.IsArchived.Value.ShouldBeFalse();
//                // ReSharper restore PossibleInvalidOperationException
//            }

//            [TestMethod]
//            public void Defaults_IsDeleted_ToFalse()
//            {
//                // arrange
//                var mockQuery = new Mock<RevisableEntityQueryCriteria<RevisableEntity>>();

//                // act
//                var query = mockQuery.Object;

//                // assert
//                query.ShouldNotBeNull();
//                query.IsDeleted.HasValue.ShouldBeTrue();
//                // ReSharper disable PossibleInvalidOperationException
//                query.IsDeleted.Value.ShouldBeFalse();
//                // ReSharper restore PossibleInvalidOperationException
//            }
//        }

//        [TestClass]
//        public class IsCurrentProperty
//        {
//            [TestMethod]
//            public void CanBeSet()
//            {
//                // arrange
//                var mockQuery = new Mock<RevisableEntityQueryCriteria<RevisableEntity>>();
//                var query = mockQuery.Object;
//                query.ShouldNotBeNull();

//                // act
//                query.IsCurrent = false;

//                // ReSharper disable ConditionIsAlwaysTrueOrFalse
//                query.IsCurrent.HasValue.ShouldBeTrue();
//                // ReSharper restore ConditionIsAlwaysTrueOrFalse
//                // ReSharper disable PossibleInvalidOperationException
//                query.IsCurrent.Value.ShouldBeFalse();
//                // ReSharper restore PossibleInvalidOperationException
//            }
//        }

//        [TestClass]
//        public class IsArchivedProperty
//        {
//            [TestMethod]
//            public void CanBeSet()
//            {
//                // arrange
//                var mockQuery = new Mock<RevisableEntityQueryCriteria<RevisableEntity>>();
//                var query = mockQuery.Object;
//                query.ShouldNotBeNull();

//                // act
//                query.IsArchived = true;

//                // ReSharper disable ConditionIsAlwaysTrueOrFalse
//                query.IsArchived.HasValue.ShouldBeTrue();
//                // ReSharper restore ConditionIsAlwaysTrueOrFalse
//                // ReSharper disable PossibleInvalidOperationException
//                query.IsArchived.Value.ShouldBeTrue();
//                // ReSharper restore PossibleInvalidOperationException
//            }
//        }

//        [TestClass]
//        public class IsDeletedProperty
//        {
//            [TestMethod]
//            public void CanBeSet()
//            {
//                // arrange
//                var mockQuery = new Mock<RevisableEntityQueryCriteria<RevisableEntity>>();
//                var query = mockQuery.Object;
//                query.ShouldNotBeNull();

//                // act
//                query.IsDeleted = true;

//                // ReSharper disable ConditionIsAlwaysTrueOrFalse
//                query.IsDeleted.HasValue.ShouldBeTrue();
//                // ReSharper restore ConditionIsAlwaysTrueOrFalse
//                // ReSharper disable PossibleInvalidOperationException
//                query.IsDeleted.Value.ShouldBeTrue();
//                // ReSharper restore PossibleInvalidOperationException
//            }
//        }

//        [TestClass]
//        public class OrderByDescendingMethod
//        {
//            [TestMethod]
//            public void AddsNoExpressions_WhenArgIsNull()
//            {
//                // arrange
//                var mockQuery = new Mock<RevisableEntityQueryCriteria<RevisableEntity>>();
//                var query = mockQuery.Object;
//                query.ShouldNotBeNull();

//                // act
//                query = query.OrderByDescending(null);

//                query.ToBeOrderedBy.ShouldNotBeNull();
//                query.ToBeOrderedBy.Count.ShouldEqual(0);
//            }
//        }
//    }
//}
