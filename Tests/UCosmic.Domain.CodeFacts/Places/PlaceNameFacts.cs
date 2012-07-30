using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Languages;

namespace UCosmic.Domain.Places
{
    public static class PlaceNameFacts
    {
        [TestClass]
        public class IsPreferredTranslationProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const bool value = true;
                var entity = new PlaceName { IsPreferredTranslation = value };
                entity.ShouldNotBeNull();
                entity.IsPreferredTranslation.ShouldEqual(value);
            }
        }

        [TestClass]
        public class TranslationToHintProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const string value = "text";
                var entity = new PlaceName { TranslationToHint = value };
                entity.ShouldNotBeNull();
                entity.TranslationToHint.ShouldEqual(value);
            }
        }

        [TestClass]
        public class NameForProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new Place();
                var entity = new PlaceName { NameFor = value };
                entity.ShouldNotBeNull();
                entity.NameFor.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new PlaceNameRuntimeEntity();
            }
            private class PlaceNameRuntimeEntity : PlaceName
            {
                public override Place NameFor
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
            public void IsVirtual()
            {
                new PlaceNameRuntimeEntity();
            }
            private class PlaceNameRuntimeEntity : PlaceName
            {
                public override Language TranslationToLanguage
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class TextProperty
        {
            [TestMethod]
            public void ChangesAsciiEquivalent_ToNull_WhenSet_WithAsciiValue()
            {
                const string value = "text";
                var entity = new PlaceName { Text = value };
                entity.AsciiEquivalent.ShouldBeNull();
            }

            [TestMethod]
            public void ChangesAsciiEquivalent_ToAscii_WhenSet_WithNonAsciiValue()
            {
                const string value = "Español";
                var entity = new PlaceName { Text = value };
                entity.AsciiEquivalent.ShouldEqual("Espanol");
            }
        }
    }
}
