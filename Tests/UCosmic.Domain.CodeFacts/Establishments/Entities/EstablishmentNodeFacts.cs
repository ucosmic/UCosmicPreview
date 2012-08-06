using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Establishments
{
    public static class EstablishmentNodeFacts
    {
        [TestClass]
        public class AncestorIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 3;
                var entity = new EstablishmentNode { AncestorId = value };
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
                var value = new Establishment();
                var entity = new EstablishmentNode { Ancestor = value };
                entity.ShouldNotBeNull();
                entity.Ancestor.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                var entity = new EstablishmentNodeRuntimeEntity();
                entity.ShouldNotBeNull();
            }
            private class EstablishmentNodeRuntimeEntity : EstablishmentNode
            {
                public override Establishment Ancestor
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
                var entity = new EstablishmentNode { OffspringId = value };
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
                var value = new Establishment();
                var entity = new EstablishmentNode { Offspring = value };
                entity.ShouldNotBeNull();
                entity.Offspring.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                var entity = new EstablishmentNodeRuntimeEntity();
                entity.ShouldNotBeNull();
            }
            private class EstablishmentNodeRuntimeEntity : EstablishmentNode
            {
                public override Establishment Offspring
                {
                    get { return null; }
                    protected internal set { }
                }
            }
        }
    }
}
