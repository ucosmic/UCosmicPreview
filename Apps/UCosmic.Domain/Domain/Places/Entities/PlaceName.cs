using UCosmic.Domain.Languages;

namespace UCosmic.Domain.Places
{
    public class PlaceName : RevisableEntity
    {
        protected internal PlaceName()
        {
        }

        public virtual Place NameFor { get; protected internal set; }
        public virtual Language TranslationToLanguage { get; protected internal set; }

        public string TranslationToHint { get; protected internal set; }
        public bool IsPreferredTranslation { get; protected internal set; }

        public string Text
        {
            get { return _text; }
            protected internal set
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

        public string AsciiEquivalent { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", TranslationToHint, Text);
        }
    }
}