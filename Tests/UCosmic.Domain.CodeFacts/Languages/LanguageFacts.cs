using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Languages
{
    public static class LanguageFacts
    {
        [TestClass]
        public class TranslateNameToMethod
        {
            [TestMethod]
            public void ReturnsNull_WhenNoNamesExist()
            {
                var language = new Language { Names = new List<LanguageName>() };
                var translatedName = language.TranslateNameTo("en");
                translatedName.ShouldBeNull();
            }
        }

        [TestClass]
        public class NativeNameProperty
        {
            [TestMethod]
            public void ReturnsNull_WhenNoNamesExist()
            {
                var language = new Language { Names = new List<LanguageName>() };
                var translatedName = language.NativeName;
                translatedName.ShouldBeNull();
            }
        }

        [TestClass]
        public class NamesProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new LanguageRuntimeEntity();
            }
            private class LanguageRuntimeEntity : Language
            {
                public override ICollection<LanguageName> Names
                {
                    get { return null; }
                    set { }
                }
            }
        }
    }
}
