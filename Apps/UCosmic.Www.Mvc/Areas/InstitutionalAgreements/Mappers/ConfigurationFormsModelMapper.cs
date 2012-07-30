using System.Linq;
using AutoMapper;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ConfigurationForms;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Mappers
{
    public static class ConfigurationFormsModelMapper
    {
        public class EntityToInstitutionalAgreementConfigurationFormProfile : Profile
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

        public class EntityFromInstitutionalAgreementConfigurationFormProfile : Profile
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