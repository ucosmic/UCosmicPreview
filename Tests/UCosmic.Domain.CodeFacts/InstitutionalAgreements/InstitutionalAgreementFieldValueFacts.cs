using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public static class InstitutionalAgreementFieldValueFacts
    {
        [TestClass]
        public class IdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 1;
                var entity = new InstitutionalAgreementTypeValue { Id = value };
                entity.ShouldNotBeNull();
                entity.Id.ShouldEqual(value);
            }
        }

        [TestClass]
        public class PersonIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 1;
                var entity = new InstitutionalAgreementTypeValue { ConfigurationId = value };
                entity.ShouldNotBeNull();
                entity.ConfigurationId.ShouldEqual(value);
            }
        }

        [TestClass]
        public class ConfigurationProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new InstitutionalAgreementConfiguration();
                var entity = new InstitutionalAgreementTypeValue { Configuration = value };
                entity.ShouldNotBeNull();
                entity.Configuration.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                var entity = new InstitutionalAgreementFieldValueRuntimeEntity();
                entity.ShouldNotBeNull();
            }
            private class InstitutionalAgreementFieldValueRuntimeEntity : InstitutionalAgreementFieldValue
            {
                public override InstitutionalAgreementConfiguration Configuration
                {
                    get { return null; }
                    protected internal set { }
                }
            }
        }
    }
}
