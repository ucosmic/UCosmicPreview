using System;
using System.Text;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementUmbrellaOption
    {
        public Guid EntityId { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public bool IsTitleDerived { get; set; }

        public string Title { get; set; }

        public string ShortTitle
        {
            get
            {
                if (!IsTitleDerived)
                    return Title;

                var sb = new StringBuilder();
                var typeWords = Type.Split(' ');
                foreach (var typeWord in typeWords)
                {
                    sb.Append(typeWord.Substring(0, 1));
                }
                sb.Append(": ");
                var endOfBetween = Title.IndexOf(" between", System.StringComparison.Ordinal) + 9;
                sb.Append(Title.Substring(endOfBetween, Title.IndexOf(" - Status is", System.StringComparison.Ordinal) - endOfBetween));
                sb.Append(string.Format(" ({0})", Status));
                return sb.ToString();
            }
        }
    }
}