using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Establishments
{
    public static class EstablishmentTypeFacts
    {
        [TestClass]
        public class CategoryIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 4;
                var entity = new EstablishmentType { CategoryId = value };
                entity.ShouldNotBeNull();
                entity.CategoryId.ShouldEqual(value);
            }
        }

        [TestClass]
        public class CategoryProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EstablishmentTypeRuntimeEntity();
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
