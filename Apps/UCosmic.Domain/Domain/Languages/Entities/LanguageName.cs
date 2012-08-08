namespace UCosmic.Domain.Languages
{
    public class LanguageName : Entity, IAmNumbered
    {
        protected internal LanguageName()
        {
        }

        public int LanguageId { get; protected internal set; }
        public int Number { get; protected internal set; }
        public virtual Language Owner { get; set; }

        public int TranslationToLanguageId { get; protected internal set; }
        public virtual Language TranslationToLanguage { get; protected internal set; }

        public string Text
        {
            get { return _text; }
            protected internal set
            {
                _text = value;
                if (string.IsNullOrWhiteSpace(_text))
                    _text = null;

                AsciiEquivalent = string.Empty;
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

        public override string ToString()
        {
            return Text;
        }
    }
}