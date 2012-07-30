using System;
using System.Text;
using AutoMapper;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Www.Mvc.Models;

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
                var endOfBetween = Title.IndexOf(" between", StringComparison.Ordinal) + 9;
                sb.Append(Title.Substring(endOfBetween, Title.IndexOf(" - Status is", StringComparison.Ordinal) - endOfBetween));
                sb.Append(string.Format(" ({0})", Status));
                return sb.ToString();
            }
        }
    }

    public static class InstitutionalAgreementUmbrellaOptionProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(InstitutionalAgreementUmbrellaOptionProfiler));
        }

        internal class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreement, InstitutionalAgreementUmbrellaOption>();
            }
        }
    }
}