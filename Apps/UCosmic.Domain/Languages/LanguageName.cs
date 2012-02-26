using System.ComponentModel.DataAnnotations;

namespace UCosmic.Domain.Languages
{
    public class LanguageName : RevisableEntity
    {
        public virtual Language NameForLanguage { get; set; }

        public virtual Language TranslationToLanguage { get; set; }

        [Required]
        [StringLength(150)]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                if (string.IsNullOrWhiteSpace(_text))
                    _text = null;

                AsciiEquivalent = null;
                if (!string.IsNullOrWhiteSpace(Text))
                {
                    var asciiEquivalent = Text.ConvertToAscii();
                    if (asciiEquivalent != null
                        && !asciiEquivalent.Equals(Text)
                        && !asciiEquivalent.ContainsOnlyQuestionMarksAndWhiteSpace())
                    {
                        AsciiEquivalent = asciiEquivalent;
                    }
                }
            }
        }
        private string _text;

        [StringLength(200)]
        public string AsciiEquivalent { get; private set; }

    }
}