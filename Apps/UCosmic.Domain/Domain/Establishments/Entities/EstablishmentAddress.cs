using UCosmic.Domain.Languages;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentAddress : RevisableEntity
    {
        protected internal EstablishmentAddress()
        {
        }

        public virtual Language TranslationToLanguage { get; protected internal set; }
        public string Text { get; protected internal set; }
    }
}