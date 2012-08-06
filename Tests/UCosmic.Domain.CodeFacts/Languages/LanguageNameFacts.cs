using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Languages
{
    public static class LanguageNameFacts
    {
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
                var entity = new LanguageNameRuntimeEntity();
                entity.ShouldNotBeNull();
            }
            private class LanguageNameRuntimeEntity : LanguageName
            {
                public override Language TranslationToLanguage
                {
                    get { return null; }
                    protected internal set { }
                }
            }
        }
    }
}
