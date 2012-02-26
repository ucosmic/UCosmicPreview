using System.ComponentModel.DataAnnotations;
using UCosmic.Domain.Languages;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentAddress : RevisableEntity
    {
        public virtual Language TranslationToLanguage { get; set; }

        [Required, StringLength(500)]
        public string Text { get; set; }
    }
}