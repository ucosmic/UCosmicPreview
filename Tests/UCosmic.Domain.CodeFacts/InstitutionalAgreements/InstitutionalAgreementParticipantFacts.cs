using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public static class InstitutionalAgreementParticipantFacts
    {
        [TestClass]
        public class IdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 1;
                var entity = new InstitutionalAgreementParticipant { Id = value };
                entity.ShouldNotBeNull();
                entity.Id.ShouldEqual(value);
            }
        }

        [TestClass]
        public class AgreementProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new InstitutionalAgreement();
                var entity = new InstitutionalAgreementParticipant { Agreement = value };
                entity.ShouldNotBeNull();
                entity.Agreement.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                var entity = new InstitutionalAgreementParticipantRuntimeEntity();
                entity.ShouldNotBeNull();
            }
            private class InstitutionalAgreementParticipantRuntimeEntity : InstitutionalAgreementParticipant
            {
                public override InstitutionalAgreement Agreement
                {
                    get { return null; }
                    protected internal set { }
                }
            }
        }

        [TestClass]
        public class EstablishmentProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new Establishment();
                var entity = new InstitutionalAgreementParticipant { Establishment = value };
                entity.ShouldNotBeNull();
                entity.Establishment.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                var entity = new InstitutionalAgreementParticipantRuntimeEntity();
                entity.ShouldNotBeNull();
            }
            private class InstitutionalAgreementParticipantRuntimeEntity : InstitutionalAgreementParticipant
            {
                public override Establishment Establishment
                {
                    get { return null; }
                    protected internal set { }
                }
            }
        }
    }
}
