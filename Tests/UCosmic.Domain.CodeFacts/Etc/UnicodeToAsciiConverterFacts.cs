using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain
{
    // ReSharper disable UnusedMember.Global
    public class UnicodeToAsciiConverterFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class ConvertToAscii_ExtensionMethod
        {
            [TestMethod]
            public void Converts_Kɔngɔ́_WithoutQuestionMark()
            {
                const string text = "Kɔngɔ́ (Kinshasa)";

                var ascii = text.ConvertToAscii();

                ascii.ShouldEqual("Kongo (Kinshasa)");
            }

            [TestMethod]
            public void Converts_MinţaqahBaḩralGhazāl_WithoutQuestionMark()
            {
                const string text = "Minţaqah Baḩr al Ghazāl";

                var ascii = text.ConvertToAscii();

                ascii.ShouldEqual("Mintaqah Bahr al Ghazal");
            }

            [TestMethod]
            public void Converts_MinţaqahAlBaţḩah_WithoutQuestionMark()
            {
                const string text = "Minţaqah Al Baţḩah";

                var ascii = text.ConvertToAscii();

                ascii.ShouldEqual("Mintaqah Al Bathah");
            }

            [TestMethod]
            public void Converts_MinţaqahAlBuḩayrah_WithoutQuestionMark()
            {
                const string text = "Minţaqah Al Buḩayrah";

                var ascii = text.ConvertToAscii();

                ascii.ShouldEqual("Mintaqah Al Buhayrah");
            }

            [TestMethod]
            public void Converts_ShabīyatalJabalalAkhḑar_WithoutQuestionMark()
            {
                const string text = "Sha‘bīyat al Jabal al Akhḑar";

                var ascii = text.ConvertToAscii();

                ascii.ShouldEqual("Sha'biyat al Jabal al Akhdar");
            }

            [TestMethod]
            public void Converts_S̲h̲arqBaḩralGhazal__WithoutQuestionMark()
            {
                const string text = "S̲h̲arq Baḩr al Ghazal";

                var ascii = text.ConvertToAscii();

                ascii.ShouldEqual("Sharq Bahr al Ghazal");
            }
        }
    }
}
