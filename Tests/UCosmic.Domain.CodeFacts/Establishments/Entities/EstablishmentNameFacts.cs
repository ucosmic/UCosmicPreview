using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Languages;

namespace UCosmic.Domain.Establishments
{
    public static class EstablishmentNameFacts
    {
        [TestClass]
        public class ForEstablishmentProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new Establishment();
                var entity = new EstablishmentName { ForEstablishment = value };
                entity.ShouldNotBeNull();
                entity.ForEstablishment.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new EstablishmentNameRuntimeEntity();
            }
            private class EstablishmentNameRuntimeEntity : EstablishmentName
            {
                public override Establishment ForEstablishment
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class TranslationToLanguageProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new Language();
                var entity = new EstablishmentName { TranslationToLanguage = value };
                entity.ShouldNotBeNull();
                entity.TranslationToLanguage.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new EstablishmentNameRuntimeEntity();
            }
            private class EstablishmentNameRuntimeEntity : EstablishmentName
            {
                public override Language TranslationToLanguage
                {
                    get { return null; }
                    set { }
                }
            }
        }
    }

}
