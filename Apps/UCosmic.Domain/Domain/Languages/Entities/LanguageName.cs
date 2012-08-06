namespace UCosmic.Domain.Languages
{
    public class LanguageName : RevisableEntity
    {
        protected internal LanguageName()
        {
        }

        public virtual Language NameForLanguage { get; protected internal set; }
        public virtual Language TranslationToLanguage { get; protected internal set; }

        public string Text
        {
            get { return _text; }
            protected internal set
            {
                _text = value;
                if (string.IsNullOrWhiteSpace(_text))
                    _text = null;

                AsciiEquivalent = null;
                if (string.IsNullOrWhiteSpace(Text)) return;

                var asciiEquivalent = Text.ConvertToAscii();
                if (asciiEquivalent != null
                    && !asciiEquivalent.Equals(Text)
                    && !asciiEquivalent.ContainsOnlyQuestionMarksAndWhiteSpace())
                {
                    AsciiEquivalent = asciiEquivalent;
                }
            }
        }
        private string _text;

        public string AsciiEquivalent { get; private set; }

    }
}