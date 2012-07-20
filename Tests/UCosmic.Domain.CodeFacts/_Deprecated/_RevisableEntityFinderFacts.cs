//using System;
//using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace UCosmic.Domain
//{
//    // ReSharper disable UnusedMember.Global
//    public class RevisableEntityFinderFacts
//    // ReSharper restore UnusedMember.Global
//    {
//        [TestClass]
//        public class FindOne_Method
//        {
//            [TestMethod]
//            public void IsVirtual()
//            {
//                new TestRevisableEntityFinder();
//            }
//            private class TestRevisableEntityFinder : RevisableEntityFinder<RevisableEntity>
//            {
//                public TestRevisableEntityFinder() : base(null) { }
//                public override ICollection<RevisableEntity> FindMany(RevisableEntityQueryCriteria<RevisableEntity> criteria)
//                {
//                    throw new NotSupportedException();
//                }
//                public override RevisableEntity FindOne(RevisableEntityQueryCriteria<RevisableEntity> criteria)
//                {
//                    return null;
//                }
//            }
//        }
//    }
//}
