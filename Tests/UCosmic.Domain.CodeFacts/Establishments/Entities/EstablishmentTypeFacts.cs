using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Establishments
{
    public static class EstablishmentTypeFacts
    {
        [TestClass]
        public class CategoryCodeProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const string value = EstablishmentCategoryCode.Govt;
                var entity = new EstablishmentType { CategoryCode = value };
                entity.ShouldNotBeNull();
                entity.CategoryCode.ShouldEqual(value);
            }
        }

        [TestClass]
        public class CategoryProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                var entity = new EstablishmentTypeRuntimeEntity();
                entity.ShouldNotBeNull();
            }
            private class EstablishmentTypeRuntimeEntity : EstablishmentType
            {
                public override EstablishmentCategory Category
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class EnglishPluralNameProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const string value = "value";
                var entity = new EstablishmentType { EnglishPluralName = value };
                entity.ShouldNotBeNull();
                entity.EnglishPluralName.ShouldEqual(value);
            }
        }

    }
}
