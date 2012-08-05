using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Places
{
    public static class GeoPlanetPlaceNodeFacts
    {
        [TestClass]
        public class AncestorIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 3;
                var entity = new GeoPlanetPlaceNode { AncestorId = value };
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
                var value = new GeoPlanetPlace();
                var entity = new GeoPlanetPlaceNode { Ancestor = value };
                entity.ShouldNotBeNull();
                entity.Ancestor.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                var entity = new GeoPlanetPlaceNodeRuntimeEntity();
                entity.ShouldNotBeNull();
            }
            private class GeoPlanetPlaceNodeRuntimeEntity : GeoPlanetPlaceNode
            {
                public override GeoPlanetPlace Ancestor
                {
                    get { return null; }
                    protected internal set { }
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
                var entity = new GeoPlanetPlaceNode { OffspringId = value };
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
                var value = new GeoPlanetPlace();
                var entity = new GeoPlanetPlaceNode { Offspring = value };
                entity.ShouldNotBeNull();
                entity.Offspring.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                var entity = new GeoPlanetPlaceNodeRuntimeEntity();
                entity.ShouldNotBeNull();
            }
            private class GeoPlanetPlaceNodeRuntimeEntity : GeoPlanetPlaceNode
            {
                public override GeoPlanetPlace Offspring
                {
                    get { return null; }
                    protected internal set { }
                }
            }
        }

        [TestClass]
        public class SeparationProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 3;
                var entity = new GeoPlanetPlaceNode { Separation = value };
                entity.ShouldNotBeNull();
                entity.Separation.ShouldEqual(value);
            }
        }
    }
}
