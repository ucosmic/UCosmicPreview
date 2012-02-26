using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.InstitutionalAgreements
{
    // ReSharper disable UnusedMember.Global
    public class InstitutionalAgreementParticipantFacts
    // ReSharper restore UnusedMember.Global
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
                new InstitutionalAgreementParticipantRuntimeEntity();
            }
            private class InstitutionalAgreementParticipantRuntimeEntity : InstitutionalAgreementParticipant
            {
                public override InstitutionalAgreement Agreement
                {
                    get { return null; }
                    set { }
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
                new InstitutionalAgreementParticipantRuntimeEntity();
            }
            private class InstitutionalAgreementParticipantRuntimeEntity : InstitutionalAgreementParticipant
            {
                public override Establishment Establishment
                {
                    get { return null; }
                    set { }
                }
            }
        }

        //[TestMethod]
        //public void Entity_InstitutionalAgreementParticipant_ShouldBeConstructible()
        //{
        //    var entity = new InstitutionalAgreementParticipant
        //    {
        //        Id = 1,
        //        AgreementId = 1,
        //        Agreement = null,
        //    };

        //    entity.ShouldNotBeNull();
        //    entity.Id.ShouldEqual(1);
        //}

        //[TestMethod]
        //public void Entity_InstitutionalAgreementParticipant_ShouldBeNavigable()
        //{
        //    var entity = new InstitutionalAgreementParticipantRuntimeEntity();

        //    entity.ShouldNotBeNull();
        //}
    }

    //internal class InstitutionalAgreementParticipantRuntimeEntity : InstitutionalAgreementParticipant
    //{
    //    public override InstitutionalAgreement Agreement
    //    {
    //        get { return null; }
    //        set { }
    //    }

    //    public override Establishment Establishment
    //    {
    //        get { return null; }
    //        set { }
    //    }
    //}
}
