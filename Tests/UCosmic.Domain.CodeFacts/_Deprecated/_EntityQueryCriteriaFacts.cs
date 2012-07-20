//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Should;

//namespace UCosmic.Domain
//{
//    // ReSharper disable UnusedMember.Global
//    public class EntityQueryCriteriaFacts
//    // ReSharper restore UnusedMember.Global
//    {
//        [TestClass]
//        public class Constructor
//        {
//            [TestMethod]
//            public void Defaults_IsForInsertOrUpdate_ToFalse()
//            {
//                // arrange
//                var mockQuery = new Mock<EntityQueryCriteria<object>>(MockBehavior.Strict);

//                // act
//                var query = mockQuery.Object;

//                // assert
//                query.ShouldNotBeNull();
//                query.IsForInsertOrUpdate.ShouldBeFalse();
//            }
//        }

//        [TestClass]
//        public class OrderBy_Method
//        {
//            [TestMethod]
//            public void AddsNoExpressions_WhenArgIsNull()
//            {
//                // arrange
//                var mockQuery = new Mock<EntityQueryCriteria<object>>(MockBehavior.Strict);
//                var query = mockQuery.Object;
//                query.ShouldNotBeNull();

//                // act
//                query = query.OrderBy(null);

//                query.ToBeOrderedBy.ShouldNotBeNull();
//                query.ToBeOrderedBy.Count.ShouldEqual(0);
//            }
//        }

//        [TestClass]
//        public class OrderByDescending_Method
//        {
//            [TestMethod]
//            public void AddsNoExpressions_WhenArgIsNull()
//            {
//                // arrange
//                var mockQuery = new Mock<EntityQueryCriteria<object>>(MockBehavior.Strict);
//                var query = mockQuery.Object;
//                query.ShouldNotBeNull();

//                // act
//                query = query.OrderByDescending(null);

//                query.ToBeOrderedBy.ShouldNotBeNull();
//                query.ToBeOrderedBy.Count.ShouldEqual(0);
//            }
//        }

//        [TestClass]
//        public class EagerLoad_Method
//        {
//            [TestMethod]
//            public void AddsNoExpressions_WhenArgIsNull()
//            {
//                // arrange
//                var mockQuery = new Mock<EntityQueryCriteria<object>>(MockBehavior.Strict);
//                var query = mockQuery.Object;
//                query.ShouldNotBeNull();

//                // act
//                query = query.EagerLoad(null);

//                query.ToBeEagerLoaded.ShouldNotBeNull();
//                query.ToBeEagerLoaded.Count.ShouldEqual(0);
//            }
//        }

//        [TestClass]
//        public class ForInsertOrUpdate_Method
//        {
//            [TestMethod]
//            public void Sets_IsForInsertOrUpdate_ToTrue()
//            {
//                // arrange
//                var mockQuery = new Mock<EntityQueryCriteria<object>>(MockBehavior.Strict);
//                var query = mockQuery.Object;
//                query.ShouldNotBeNull();

//                // act
//                query = query.ForInsertOrUpdate();

//                query.IsForInsertOrUpdate.ShouldBeTrue();
//            }
//        }
//    }
//}
