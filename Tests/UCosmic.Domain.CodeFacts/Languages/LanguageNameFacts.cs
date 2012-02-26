using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Languages
{
    // ReSharper disable UnusedMember.Global
    public class LanguageNameFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class NameForLanguageProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new Language();
                var entity = new LanguageName { NameForLanguage = value };
                entity.ShouldNotBeNull();
                entity.NameForLanguage.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new LanguageNameRuntimeEntity();
            }
            private class LanguageNameRuntimeEntity : LanguageName
            {
                public override Language NameForLanguage
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
                var entity = new LanguageName { TranslationToLanguage = value };
                entity.ShouldNotBeNull();
                entity.TranslationToLanguage.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new LanguageNameRuntimeEntity();
            }
            private class LanguageNameRuntimeEntity : LanguageName
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
