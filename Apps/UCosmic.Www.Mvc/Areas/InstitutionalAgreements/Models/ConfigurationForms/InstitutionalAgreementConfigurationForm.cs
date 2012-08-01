using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ConfigurationForms
{
    public class InstitutionalAgreementConfigurationForm
    {
        public const string DuplicateOptionErrorMessage = "The option '{0}' already exists. Please do not add any duplicate options.";

        public InstitutionalAgreementConfigurationForm()
        {
            EntityId = Guid.NewGuid();
            IsCustomTypeAllowed = true;
            IsCustomStatusAllowed = true;
            IsCustomContactTypeAllowed = true;
            AllowedTypeValues = new List<InstitutionalAgreementTypeValueForm>();
            AllowedStatusValues = new List<InstitutionalAgreementStatusValueForm>();
            AllowedContactTypeValues = new List<InstitutionalAgreementContactTypeValueForm>();
        }

        [HiddenInput(DisplayValue = false)]
        public int RevisionId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid EntityId { get; set; }

        public int? ForEstablishmentId { get; set; }
        public string ForEstablishmentOfficialName { get; set; }

        public bool IsCustomTypeAllowed { get; set; }
        public bool IsCustomStatusAllowed { get; set; }
        public bool IsCustomContactTypeAllowed { get; set; }

        [Display(Name = "Agreement types")]
        [NoDuplicateValues(IgnoreCase = true, ErrorMessage = DuplicateOptionErrorMessage)]
        public List<InstitutionalAgreementTypeValueForm> AllowedTypeValues { get; set; }

        [UIHint("TypeComboBox")]
        public string ExampleTypeInput { get; set; }

        [UIHint("StatusComboBox")]
        public string ExampleStatusInput { get; set; }

        [UIHint("ContactTypeComboBox")]
        public string ExampleContactTypeInput { get; set; }

        [Display(Name = "Agreement statuses")]
        [NoDuplicateValues(IgnoreCase = true, ErrorMessage = DuplicateOptionErrorMessage)]
        public List<InstitutionalAgreementStatusValueForm> AllowedStatusValues { get; set; }

        [Display(Name = "Contact types")]
        [NoDuplicateValues(IgnoreCase = true, ErrorMessage = DuplicateOptionErrorMessage)]
        public List<InstitutionalAgreementContactTypeValueForm> AllowedContactTypeValues { get; set; }
    }

    public static class InstitutionalAgreementConfigurationFormProfiler
    {
        public class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreementConfiguration, InstitutionalAgreementConfigurationForm>()
                    .ForMember(m => m.ExampleTypeInput, opt => opt.Ignore())
                    .ForMember(m => m.ExampleStatusInput, opt => opt.Ignore())
                    .ForMember(m => m.ExampleContactTypeInput, opt => opt.Ignore())
                ;
                CreateMap<InstitutionalAgreementTypeValue, InstitutionalAgreementTypeValueForm>()
                    .ForMember(f => f.IsAdded, opt => opt.MapFrom(v => true))
                ;
                CreateMap<InstitutionalAgreementStatusValue, InstitutionalAgreementStatusValueForm>()
                    .ForMember(f => f.IsAdded, opt => opt.MapFrom(v => true))
                ;
                CreateMap<InstitutionalAgreementContactTypeValue, InstitutionalAgreementContactTypeValueForm>()
                    .ForMember(f => f.IsAdded, opt => opt.MapFrom(v => true))
                ;
            }
        }

        public class ModelToEntityProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreementConfigurationForm, InstitutionalAgreementConfiguration>()
                    .ForMember(e => e.ForEstablishment, opt => opt.Ignore())
                    .ForMember(e => e.CreatedOnUtc, opt => opt.Ignore())
                    .ForMember(e => e.CreatedByPrincipal, opt => opt.Ignore())
                    .ForMember(e => e.UpdatedOnUtc, opt => opt.Ignore())
                    .ForMember(e => e.UpdatedByPrincipal, opt => opt.Ignore())
                    .ForMember(e => e.Version, opt => opt.Ignore())
                    .ForMember(e => e.IsCurrent, opt => opt.Ignore())
                    .ForMember(e => e.IsArchived, opt => opt.Ignore())
                    .ForMember(e => e.IsDeleted, opt => opt.Ignore())
                    .ForMember(e => e.AllowedTypeValues, opt => opt
                        .ResolveUsing(source => source.AllowedTypeValues.Where(v => v.IsAdded)))
                    .ForMember(e => e.AllowedStatusValues, opt => opt
                        .ResolveUsing(source => source.AllowedStatusValues.Where(v => v.IsAdded)))
                    .ForMember(e => e.AllowedContactTypeValues, opt => opt
                        .ResolveUsing(source => source.AllowedContactTypeValues.Where(v => v.IsAdded)))
                ;
                CreateMap<InstitutionalAgreementTypeValueForm, InstitutionalAgreementTypeValue>()
                    .ForMember(e => e.Configuration, opt => opt.Ignore())
                ;
                CreateMap<InstitutionalAgreementStatusValueForm, InstitutionalAgreementStatusValue>()
                    .ForMember(e => e.Configuration, opt => opt.Ignore())
                ;
                CreateMap<InstitutionalAgreementContactTypeValueForm, InstitutionalAgreementContactTypeValue>()
                    .ForMember(e => e.Configuration, opt => opt.Ignore())
                ;
            }
        }
    }
}