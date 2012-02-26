using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.InstitutionalAgreements
{
    // ReSharper disable UnusedMember.Global
    public class InstitutionalAgreementFieldValueFacts
    // ReSharper restore UnusedMember.Global
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

            [TestMethod]
            public void HasKeyAttribute()
            {
                Expression<Func<InstitutionalAgreementTypeValue, int>> property = p => p.Id;
                var attributes = property.GetAttributes<InstitutionalAgreementTypeValue, int, KeyAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldBeType<KeyAttribute>();
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
                new InstitutionalAgreementFieldValueRuntimeEntity();
            }
            private class InstitutionalAgreementFieldValueRuntimeEntity : InstitutionalAgreementFieldValue
            {
                public override InstitutionalAgreementConfiguration Configuration
                {
                    get { return null; }
                    set { }
                }
            }
        }
    }
}
