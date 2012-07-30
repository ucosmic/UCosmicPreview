using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Places
{
    public static class PlaceNodeFacts
    {
        [TestClass]
        public class AncestorIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 3;
                var entity = new PlaceNode { AncestorId = value };
                entity.ShouldNotBeNull();
                entity.AncestorId.ShouldEqual(value);
            }
        }

        [TestClass]
        public class AncestorProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new Place();
                var entity = new PlaceNode { Ancestor = value };
                entity.ShouldNotBeNull();
                entity.Ancestor.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new PlaceNodeRuntimeEntity();
            }
            private class PlaceNodeRuntimeEntity : PlaceNode
            {
                public override Place Ancestor
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class OffspringIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 5;
                var entity = new PlaceNode { OffspringId = value };
                entity.ShouldNotBeNull();
                entity.OffspringId.ShouldEqual(value);
            }
        }

        [TestClass]
        public class OffspringProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new Place();
                var entity = new PlaceNode { Offspring = value };
                entity.ShouldNotBeNull();
                entity.Offspring.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new PlaceNodeRuntimeEntity();
            }
            private class PlaceNodeRuntimeEntity : PlaceNode
            {
                public override Place Offspring
                {
                    get { return null; }
                    set { }
                }
            }
        }
    }
}
