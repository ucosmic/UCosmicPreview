using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Places
{
    public static class GeoNamesToponymNodeFacts
    {
        [TestClass]
        public class AncestorIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 3;
                var entity = new GeoNamesToponymNode { AncestorId = value };
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
                var value = new GeoNamesToponym();
                var entity = new GeoNamesToponymNode { Ancestor = value };
                entity.ShouldNotBeNull();
                entity.Ancestor.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new GeoNamesToponymNodeRuntimeEntity();
            }
            private class GeoNamesToponymNodeRuntimeEntity : GeoNamesToponymNode
            {
                public override GeoNamesToponym Ancestor
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
                var entity = new GeoNamesToponymNode { OffspringId = value };
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
                var value = new GeoNamesToponym();
                var entity = new GeoNamesToponymNode { Offspring = value };
                entity.ShouldNotBeNull();
                entity.Offspring.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new GeoNamesToponymNodeRuntimeEntity();
            }
            private class GeoNamesToponymNodeRuntimeEntity : GeoNamesToponymNode
            {
                public override GeoNamesToponym Offspring
                {
                    get { return null; }
                    set { }
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
                var entity = new GeoNamesToponymNode { Separation = value };
                entity.ShouldNotBeNull();
                entity.Separation.ShouldEqual(value);
            }
        }
    }
}
