using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Mappers
{
    public static class SelfModelMapper
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(SelfModelMapper));
        }

        // ReSharper disable UnusedMember.Local

        //private class PersonFormProfile : Profile
        //{
        //    protected override void Configure()
        //    {
        //        // entities to models
        //        CreateMap<Person, PersonForm>()
        //            .ForMember(d => d.UserId, o => o.Ignore())
        //        ;
        //        CreateMap<Affiliation, PersonForm.AffiliationInfo>()
        //            .ForMember(d => d.EntityId, o => o.Ignore())
        //        ;
        //        CreateMap<Establishment, PersonForm.AffiliationInfo.EstablishmentInfo>();

        //        // model to entity
        //        CreateMap<PersonForm, Person>()
        //            .ForMember(target => target.User, opt => opt.Ignore())
        //            .ForMember(target => target.Emails, opt => opt.Ignore())
        //            .ForMember(target => target.Messages, opt => opt.Ignore())
        //            .ForMember(target => target.Affiliations, opt => opt.Ignore())
        //            .ForMember(target => target.CreatedOnUtc, opt => opt.Ignore())
        //            .ForMember(target => target.CreatedByPrincipal, opt => opt.Ignore())
        //            .ForMember(target => target.UpdatedOnUtc, opt => opt.Ignore())
        //            .ForMember(target => target.UpdatedByPrincipal, opt => opt.Ignore())
        //            .ForMember(target => target.Version, opt => opt.Ignore())
        //            .ForMember(target => target.IsCurrent, opt => opt.Ignore())
        //            .ForMember(target => target.IsArchived, opt => opt.Ignore())
        //            .ForMember(target => target.IsDeleted, opt => opt.Ignore())
        //        ;
        //    }
        //}

        //private class EmailInfoProfile : Profile
        //{
        //    protected override void Configure()
        //    {
        //        CreateMap<EmailAddress, EmailInfo>();
        //        //CreateMap<EmailInfo, EmailAddress>()
        //        //    .ForMember(target => target.Confirmations, opt => opt.Ignore())
        //        //    .ForMember(target => target.IsFromSaml, opt => opt.Ignore())
        //        //;
        //    }
        //}

        //private class AffiliationFormProfile : Profile
        //{
        //    protected override void Configure()
        //    {
        //        CreateMap<Affiliation, AffiliationForm>()
        //            .ForMember(target => target.EmployeeOrStudent, opt => opt.Ignore())
        //            .ForMember(target => target.ReturnUrl, opt => opt.Ignore())
        //        ;
        //        CreateMap<Establishment, AffiliationForm.EstablishmentInfo>();

        //        CreateMap<AffiliationForm, Affiliation>()
        //            .ForMember(target => target.CreatedOnUtc, opt => opt.Ignore())
        //            .ForMember(target => target.CreatedByPrincipal, opt => opt.Ignore())
        //            .ForMember(target => target.UpdatedOnUtc, opt => opt.Ignore())
        //            .ForMember(target => target.UpdatedByPrincipal, opt => opt.Ignore())
        //            .ForMember(target => target.EstablishmentId, opt => opt.Ignore())
        //            .ForMember(target => target.Establishment, opt => opt.Ignore())
        //            .ForMember(target => target.PersonId, opt => opt.Ignore())
        //            .ForMember(target => target.Person, opt => opt.Ignore())
        //            .ForMember(target => target.Version, opt => opt.Ignore())
        //            .ForMember(target => target.IsCurrent, opt => opt.Ignore())
        //            .ForMember(target => target.IsArchived, opt => opt.Ignore())
        //            .ForMember(target => target.IsDeleted, opt => opt.Ignore())
        //        ; 

        //    }
        //}

        // ReSharper restore UnusedMember.Local
    }
}