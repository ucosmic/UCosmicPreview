using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.People;

namespace UCosmic.Domain.InstitutionalAgreements
{
    // ReSharper disable UnusedMember.Global
    public class InstitutionalAgreementContactFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TypeProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const string value = "text";
                var entity = new InstitutionalAgreementContact { Type = value };
                entity.ShouldNotBeNull();
                entity.Type.ShouldEqual(value);
            }
        }

        [TestClass]
        public class AgreementProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new InstitutionalAgreement();
                var entity = new InstitutionalAgreementContact { Agreement = value };
                entity.ShouldNotBeNull();
                entity.Agreement.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new InstitutionalAgreementContactRuntimeEntity();
            }
            private class InstitutionalAgreementContactRuntimeEntity : InstitutionalAgreementContact
            {
                public override InstitutionalAgreement Agreement
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class PersonProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new Person();
                var entity = new InstitutionalAgreementContact { Person = value };
                entity.ShouldNotBeNull();
                entity.Person.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new InstitutionalAgreementContactRuntimeEntity();
            }
            private class InstitutionalAgreementContactRuntimeEntity : InstitutionalAgreementContact
            {
                public override Person Person
                {
                    get { return null; }
                    set { }
                }
            }
        }
    }
}
