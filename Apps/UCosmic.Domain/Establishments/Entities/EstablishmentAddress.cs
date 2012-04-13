using UCosmic.Domain.Languages;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentAddress : RevisableEntity
    {
        public virtual Language TranslationToLanguage { get; set; }

        public string Text { get; set; }
    }
}