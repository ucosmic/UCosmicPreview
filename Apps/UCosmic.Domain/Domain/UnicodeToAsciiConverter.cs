using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UCosmic.Domain
{
    /// <summary>
    /// Provides text conversion from unicode to ASCII equivalents.
    /// </summary>
    internal static class UnicodeToAsciiConverter
    {
        #region Mappings

        /// <summary>
        /// A string-to-string dictionary for mapping unicode text element keys to their
        /// equivalent ASCII text element values.
        /// </summary>
        private static readonly IDictionary<string, string> UnicodeToAsciiConversions = new Dictionary<string, string>
        {
            { "ʼ", SingleQuote }, { "‘", SingleQuote }, { "’", SingleQuote },  { "ʻ", SingleQuote }, { "–", "-" },
            { "‎", string.Empty }, { "¯", "_"}, { "—", "-"},

            { "á", a }, { "Á", A }, { "à", a }, { "À", A }, { "â", a }, { "Â", A }, { "ä", a }, { "Ä", A },
            { "ă", a }, { "Ă", A }, { "ā", a }, { "Ā", A }, { "ã", a }, { "Ã", A }, { "å", a }, { "Å", A },
            { "ầ", a }, { "Ầ", A }, { "ắ", a }, { "Ắ", A }, { "ằ", a }, { "Ằ", A }, { "ẵ", a }, { "Ẵ", A },
            { "ả", a }, { "Ả", A }, { "ạ", a }, { "Ạ", A }, { "ậ", a }, { "Ậ", A }, { "ấ", a }, { "Ấ", A },
            { "ą", a }, { "Ą", A },

            { "æ", "ae" }, { "Æ", "AE" }, { "ǣ", "ae" }, { "Ǣ", "AE" },
            { "ß", b }, { "þ", b }, { "Þ", B },

            { "ć", c }, { "Ć", C }, { "č", c }, { "Č", C }, { "ç", c }, { "Ç", C }, { "ĉ", c }, { "Ĉ", C },
            { "ċ", c }, { "Ċ", C },
            { "ḑ", d }, { "Ḑ", D }, { "đ", d }, { "Đ", D }, { "ð", d }, { "Ð", D }, { "ḍ", d }, { "Ḍ", D },
            { "ď", d }, { "Ď", D }, { "ḑ", d }, { "Ḑ", D },

            { "é", e }, { "É", E }, { "è", e }, { "È", E }, { "ė", e }, { "Ė", E }, { "ê", e }, { "Ê", E },
            { "ë", e }, { "Ë", E }, { "ě", e }, { "Ě", E }, { "ĕ", e }, { "Ĕ", E }, { "ē", e }, { "Ē", E },
            { "ę", e }, { "Ę", E }, { "ế", e }, { "Ế", E }, { "ề", e }, { "Ề", E }, { "ệ", e }, { "Ệ", E },
            { "ǝ", e }, { "Ǝ", E }, { "ə", e }, { "Ə", E }, { "ể", e }, { "Ể", E }, { "ễ", e }, { "Ễ", E },
            { "ẹ̀", e }, { "Ẹ̀", E }, { "ɛ́", e }, { "ɛ", e }, { "Ɛ", E },

            { "ğ", g }, { "Ğ", G }, { "ĝ", g }, { "Ĝ", G }, { "ġ", g }, { "Ġ", G }, { "ģ", g }, { "Ģ", G },
            { "ḩ", h }, { "Ḩ", H }, { "ħ", h }, { "Ħ", H }, { "ḥ", h }, { "Ḥ", H }, { "ĥ", h }, { "Ĥ", H },
            { "ẖ", h }, { "H̱", H }, { "h̲", h }, { "H̲", H }, { "ḩ", h }, { "Ḩ", H },

            { "ı", i }, { "ı".ToUpper(), I }, { "í", i }, { "Í", I }, { "ì", i }, { "Ì", I }, { "İ".ToLower(), i }, { "İ", I },
            { "î", i }, { "Î", I }, { "ï", i }, { "Ï", I }, { "ĭ", i }, { "Ĭ", I }, { "ī", i }, { "Ī", I },
            { "ĩ", i }, { "Ĩ", I }, { "ỉ", i }, { "Ỉ", I }, { "ị", i }, { "Ị", I },
            { "ķ", k }, { "Ķ", K },
            { "ļ", l }, { "Ļ", L }, { "ł", l }, { "Ł", L }, { "ľ", l }, { "Ľ", L },
            { "ň", n }, { "Ň", N }, { "ñ", n }, { "Ñ", N }, { "ń", n }, { "Ń", N }, { "ŋ", n }, { "Ŋ", N },
            { "ņ", n }, { "Ņ", N },

            { "ó", o }, { "Ó", O }, { "ò", o }, { "Ò", O }, { "ô", o }, { "Ô", O }, { "ö", o }, { "Ö", O },
            { "ŏ", o }, { "Ŏ", O }, { "ō", o }, { "Ō", O }, { "õ", o }, { "Õ", O }, { "ő", o }, { "Ő", O },
            { "ố", o }, { "Ố", O }, { "ồ", o }, { "Ồ", O }, { "ø", o }, { "Ø", O }, { "ơ", o }, { "Ơ", O },
            { "ọ", o }, { "Ọ", O }, { "ớ", o }, { "Ớ", O }, { "ộ", o }, { "Ộ", O }, { "ɔ", o }, { "Ɔ", O },
            { "ɔ́", o }, { "Ɔ́", O }, { "ổ", o }, { "Ổ", O }, { "ỏ", o }, { "Ỏ", O },

            { "œ", oe }, { "Œ", OE }, { "œ̆", oe }, { "Œ̆", OE },

            { "ř", r }, { "Ř", R },
            { "ś", s }, { "Ś", S }, { "š", s }, { "Š", S }, { "ş", s }, { "Ş", S }, { "ṣ", s }, { "Ṣ", S },
            { "ŝ", s }, { "Ŝ", S }, { "ș", s }, { "s̲", s }, { "S̲", S },
            { "ţ", t }, { "Ţ", T }, { "ṭ", t }, { "Ṭ", T }, { "ŧ", t }, { "Ŧ", T }, { "ț", t }, { "ť", t }, { "Ť", T },

            { "ú", u }, { "Ú", U }, { "ù", u }, { "Ù", U }, { "ü", u }, { "Ü", U }, { "ŭ", u }, { "Ŭ", U },
            { "ū", u }, { "Ū", U }, { "ũ", u }, { "Ũ", U }, { "ų", u }, { "Ų", U }, { "ủ", u }, { "Ủ", U },
            { "ư", u }, { "Ư", U }, { "ừ", u }, { "Ừ", U }, { "û", u }, { "Û", U }, { "ự", u }, { "Ự", U },
            { "ů", u }, { "Ů", U }, { "ụ", u }, { "Ụ", U }, { "ṳ", u }, { "Ṳ", U }, { "ứ", u }, { "Ứ", U },
            { "ŵ", w }, { "Ŵ", W },
            { "ý", y }, { "Ý", Y }, { "ỹ", y }, { "Ỹ", Y }, { "ỳ", y }, { "Ỳ", Y },
            { "ź", z }, { "Ź", Z }, { "ž", z }, { "Ž", Z }, { "z̄", z }, { "Z̄", Z }, { "z̧", z }, { "Z̧", Z },
            { "ż", z }, { "Ż", Z }, { "ẕ", z }, { "Ẕ", Z },

        };

        #region ASCII Constants

        // ReSharper disable InconsistentNaming
        // ascii constants save memory for immutable strings
        private const string SingleQuote = "'";
        private const string a = "a";
        private const string A = "A";
        private const string b = "b";
        private const string B = "B";
        private const string c = "c";
        private const string C = "C";
        private const string d = "d";
        private const string D = "D";
        private const string e = "e";
        private const string E = "E";
        private const string g = "g";
        private const string G = "G";
        private const string h = "h";
        private const string H = "H";
        private const string i = "i";
        private const string I = "I";
        private const string k = "k";
        private const string K = "K";
        private const string l = "l";
        private const string L = "L";
        private const string n = "n";
        private const string N = "N";
        private const string o = "o";
        private const string O = "O";
        private const string oe = "oe";
        private const string OE = "OE";
        private const string r = "r";
        private const string R = "R";
        private const string s = "s";
        private const string S = "S";
        private const string t = "t";
        private const string T = "T";
        private const string u = "u";
        private const string U = "U";
        private const string w = "w";
        private const string W = "W";
        private const string y = "y";
        private const string Y = "Y";
        private const string z = "z";
        private const string Z = "Z";
        // ReSharper restore InconsistentNaming

        #endregion
        #endregion
        #region Conversion

        /// <summary>
        /// Converts unicode text to its ASCII equivalent using a 2-pass algorithm.
        /// <para>
        /// In the first pass, the <paramref name="unicodeText"/> parameter is searched
        /// for text present in the <see cref="UnicodeToAsciiConversions"/> dictionary.
        /// For each match, the unicode text is replaced with its equivalent ASCII text
        /// from the dictionary. <em>It cannot be guaranteed that each non-ASCII character
        /// will be converted after the first pass.</em>
        /// </para>
        /// <para>
        /// The second pass guarantees that the converted text will be ASCII-compatible.
        /// This is achieved by replacing all incompatible charaters with a question mark
        /// (?) character.
        /// </para>
        /// </summary>
        /// <param name="unicodeText">
        /// The unicode text to convert to an ASCII equivalent.
        /// </param>
        /// <returns>
        /// The ASCII equivalent of the <paramref name="unicodeText"/> value.
        /// </returns>
        private static string Convert(string unicodeText)
        {
            if (string.IsNullOrWhiteSpace(unicodeText))
                return unicodeText;

            var asciiBuilder = new StringBuilder(unicodeText);
            foreach (var conversion in UnicodeToAsciiConversions)
            {
                if (unicodeText.Contains(conversion.Key))
                {
                    asciiBuilder.Replace(conversion.Key, conversion.Value);
                }
            }
            asciiBuilder.Replace("·", string.Empty);
            asciiBuilder.Replace("‎", string.Empty);
            var utf8Encoding = new UTF8Encoding();
            var asciiEncoding = new ASCIIEncoding();
            var convertedToAsciiPass1 = asciiBuilder.ToString();
            var utfBytes = utf8Encoding.GetBytes(convertedToAsciiPass1);
            if (utfBytes.Contains((byte)204) && utfBytes.Contains((byte)129))
            {
                var utfList = utfBytes.ToList();
                while (utfList[utfList.IndexOf(204) + 1] == 129)
                {
                    utfList.RemoveAt(utfList.IndexOf(204) + 1);
                    utfList.RemoveAt(utfList.IndexOf(204));
                }
                utfBytes = utfList.ToArray();
            }
            var asciiBytes = Encoding.Convert(utf8Encoding, asciiEncoding, utfBytes);
            var convertedToAsciiPass2 = asciiEncoding.GetString(asciiBytes);
            return convertedToAsciiPass2;
        }

        #endregion
        #region String Extension Methods

        /// <summary>
        /// Converts unicode text to its ASCII equivalent using the
        /// <see cref="UnicodeToAsciiConverter.Convert(System.String)"/> implementation.
        /// </summary>
        /// <remarks>
        /// This is simply a shortcut to provide a more fluent API when converting
        /// unicode text values to their ASCII equivalents.
        /// </remarks>
        /// <returns>
        /// The ASCII equivalent of the this text value.
        /// </returns>
        internal static string ConvertToAscii(this string unicodeText)
        {
            return Convert(unicodeText);
        }

        /// <summary>
        /// Determines whether a string of text contains only question marks and
        /// whitespace characters. This is useful in determining whether a conversion
        /// from unicode to ASCII failed completely, as often happens with languages
        /// like Arabic.
        /// </summary>
        /// <returns>
        /// <code>True</code> if this string of text contains only question marks and whitespace
        /// characters, otherwise <code>false</code>.
        /// </returns>
        internal static bool ContainsOnlyQuestionMarksAndWhiteSpace(this string text)
        {
            return text.All(character => character == '?' || character == ' ' || character == '\''
                                         || character == '-' || character == '_' || character == '(' || character == ')'
                                         || character == ',' || character == '/' || character == '.' || character == '&'
                                         || character == '"');
        }

        #endregion
    }
}