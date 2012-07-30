using System;
using System.Web.Mvc;
using AutoMapper;
using UCosmic.Domain.InstitutionalAgreements;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementFileInfo
    {
        [HiddenInput(DisplayValue = false)]
        public int RevisionId { get; set; }

        public string Name { get; set; }

        public string MimeType { get; set; }
        public int Length { get; set; }

        public Guid EntityId { get; set; }
        public Guid AgreementEntityId { get; set; }
    }

    public static class InstitutionalAgreementFileProfiler
    {
        public class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreementFile, InstitutionalAgreementFileInfo>()
                ;
            }
        }
    }
}