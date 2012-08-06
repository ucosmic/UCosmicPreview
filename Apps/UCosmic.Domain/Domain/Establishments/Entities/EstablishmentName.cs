using UCosmic.Domain.Languages;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentName : RevisableEntity
    {
        protected internal EstablishmentName()
        {
        }

        public virtual Establishment ForEstablishment { get; protected internal set; }
        public virtual Language TranslationToLanguage { get; protected internal set; }

        public bool IsFormerName { get; protected internal set; }
        public bool IsOfficialName { get; protected internal set; }

        public string Text
        {
            get { return _text; }
            protected internal set
            {
                _text = (!string.IsNullOrWhiteSpace(value)) ? value : null;

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

        public override string ToString()
        {
            return Text;
        }

    }
}