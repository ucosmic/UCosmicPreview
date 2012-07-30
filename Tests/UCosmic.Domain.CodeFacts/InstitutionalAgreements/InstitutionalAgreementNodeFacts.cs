using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public static class InstitutionalAgreementNodeFacts
    {
        [TestClass]
        public class AncestorIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 9;
                var entity = new InstitutionalAgreementNode { AncestorId = value };
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
                var value = new InstitutionalAgreement();
                var entity = new InstitutionalAgreementNode { Ancestor = value };
                entity.ShouldNotBeNull();
                entity.Ancestor.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new InstitutionalAgreementNodeRuntimeEntity();
            }
            private class InstitutionalAgreementNodeRuntimeEntity : InstitutionalAgreementNode
            {
                public override InstitutionalAgreement Ancestor
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
                const int value = 13;
                var entity = new InstitutionalAgreementNode { OffspringId = value };
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
                var value = new InstitutionalAgreement();
                var entity = new InstitutionalAgreementNode { Offspring = value };
                entity.ShouldNotBeNull();
                entity.Offspring.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new InstitutionalAgreementNodeRuntimeEntity();
            }
            private class InstitutionalAgreementNodeRuntimeEntity : InstitutionalAgreementNode
            {
                public override InstitutionalAgreement Offspring
                {
                    get { return null; }
                    set { }
                }
            }
        }
    }
}
