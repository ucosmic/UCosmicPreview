using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UCosmic.Domain.Languages;

namespace UCosmic.Domain.Places
{
    public class PlaceName : RevisableEntity
    {
        public virtual Place NameFor { get; set; }

        public virtual Language TranslationToLanguage { get; set; }

        [StringLength(10)]
        public string TranslationToHint { get; set; }

        [DefaultValue(false)]
        public bool IsPreferredTranslation { get; set; }

        [Required]
        [StringLength(200)]
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

        [StringLength(250)]
        public string AsciiEquivalent { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", TranslationToHint, Text);
        }
    }
}