using System.Linq;
using UCosmic.Domain.Languages;
using System.Collections.Generic;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentName : RevisableEntity
    {
        public virtual Establishment ForEstablishment { get; set; }

        public string TranslationToHint { get; set; }

        public virtual Language TranslationToLanguage { get; set; }

        public bool IsFormerName { get; set; }

        public bool IsOfficialName { get; set; }

        public string Text
        {
            get { return _text; }
            set
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

    public static class EstablishmentNameExtensions
    {
        public static IEnumerable<EstablishmentName> WhereIsNotOfficialName(this IEnumerable<EstablishmentName> enumerable)
        {
            return enumerable.Where(x => !x.IsOfficialName);
        }
    }
}