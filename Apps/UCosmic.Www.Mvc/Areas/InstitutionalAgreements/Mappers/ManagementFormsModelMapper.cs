using System.Linq;
using AutoMapper;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Files;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Mappers
{
    public static class ManagementFormsModelMapper
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(ManagementFormsModelMapper));
        }

        // ReSharper disable UnusedMember.Local
        // ReSharper disable ClassNeverInstantiated.Local

        private class EntityToInstitutionalAgreementSearchResultProfile : Profile
        {
            protected override void Configure()
            {
                // convert entity to model
                CreateMap<InstitutionalAgreement, InstitutionalAgreementSearchResult>()
                    //.ForMember(dto => dto.Files, o => o.ResolveUsing<InstitutionalAgreementToSearchResultFilesResolver>())
                ;
            }
        }

        private class EntityToInstitutionalAgreementMapSearchResultProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreement, InstitutionalAgreementMapSearchResult>();
            }
        }

        private class EntityToInstitutionalAgreementParticipantMarkerProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<EstablishmentLocation, InstitutionalAgreementParticipantMarker>()
                    .ForMember(t => t.Agreements, o => o.Ignore())
                ;
            }
        }

        private class EntityToInstitutionalAgreementFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreement, InstitutionalAgreementForm>()
                    .ForMember(d => d.ReturnUrl, o => o.Ignore())
                    //.ForMember(d => d.Files, o => o.ResolveUsing<InstitutionalAgreementToFormFilesResolver>())
                    //.ForMember(d => d.Contacts, o => o.ResolveUsing<InstitutionalAgreementToFormContactsResolver>())
                    .ForMember(d => d.Umbrella, o => o
                        .ResolveUsing(s => s.Umbrella != null
                            ? Mapper.Map<InstitutionalAgreementForm.UmbrellaForm>(s.Umbrella)
                            : new InstitutionalAgreementForm.UmbrellaForm()))
                ;
                CreateMap<InstitutionalAgreement, InstitutionalAgreementForm.UmbrellaForm>()
                    .ForMember(target => target.Options, o => o.Ignore())
                ;
            }

            //private class InstitutionalAgreementToFormFilesResolver : ValueResolver<InstitutionalAgreement, IList<InstitutionalAgreementFileForm>>
            //{
            //    protected override IList<InstitutionalAgreementFileForm> ResolveCore(InstitutionalAgreement source)
            //    {
            //        if (source.Files == null) return null;

            //        var currentEntities = source.Files.Current();
            //        var currentModels = Mapper.Map<IList<InstitutionalAgreementFileForm>>(currentEntities);
            //        return currentModels;
            //    }
            //}

            //private class InstitutionalAgreementToFormContactsResolver : ValueResolver<InstitutionalAgreement, IList<InstitutionalAgreementContactForm>>
            //{
            //    protected override IList<InstitutionalAgreementContactForm> ResolveCore(InstitutionalAgreement source)
            //    {
            //        if (source.Contacts == null) return null;

            //        var currentEntities = source.Contacts.Current();
            //        var currentModels = Mapper.Map<IList<InstitutionalAgreementContactForm>>(currentEntities);
            //        return currentModels;
            //    }
            //}
        }

        //private class EntityFromInstitutionalAgreementFormProfile : Profile
        //{
        //    protected override void Configure()
        //    {
        //        CreateMap<InstitutionalAgreementForm, InstitutionalAgreement>()
        //            .ForMember(e => e.Umbrella, o => o.Ignore())
        //            .ForMember(e => e.Ancestors, o => o.Ignore())
        //            .ForMember(e => e.Children, o => o.Ignore())
        //            .ForMember(e => e.Offspring, o => o.Ignore())
        //            .ForMember(e => e.Contacts, o => o.Ignore())
        //            .ForMember(e => e.Participants, o => o.Ignore())
        //            .ForMember(e => e.Files, o => o.Ignore())
        //            .ForMember(e => e.CreatedOnUtc, o => o.Ignore())
        //            .ForMember(e => e.CreatedByPrincipal, o => o.Ignore())
        //            .ForMember(e => e.UpdatedOnUtc, o => o.Ignore())
        //            .ForMember(e => e.UpdatedByPrincipal, o => o.Ignore())
        //            .ForMember(e => e.Version, o => o.Ignore())
        //            .ForMember(e => e.IsCurrent, o => o.Ignore())
        //            .ForMember(e => e.IsArchived, o => o.Ignore())
        //            .ForMember(e => e.IsDeleted, o => o.Ignore())
        //            .ForMember(e => e.VisibilityText, o => o.Ignore())
        //        ;
        //    }
        //}

        private class EntityToInstitutionalAgreementUmbrellaOptionProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreement, InstitutionalAgreementUmbrellaOption>();
            }
        }

        //private class EntityFromInstitutionalAgreementDeriveTitleInputProfile : Profile
        //{
        //    protected override void Configure()
        //    {
        //        CreateMap<InstitutionalAgreementDeriveTitleInput, InstitutionalAgreement>()
        //            .ForMember(e => e.Umbrella, o => o.Ignore())
        //            .ForMember(e => e.Ancestors, o => o.Ignore())
        //            .ForMember(e => e.Children, o => o.Ignore())
        //            .ForMember(e => e.Offspring, o => o.Ignore())
        //            .ForMember(e => e.StartsOn, o => o.Ignore())
        //            .ForMember(e => e.ExpiresOn, o => o.Ignore())
        //            .ForMember(e => e.IsExpirationEstimated, o => o.Ignore())
        //            .ForMember(e => e.Description, o => o.Ignore())
        //            .ForMember(e => e.IsAutoRenew, o => o.Ignore())
        //            .ForMember(e => e.Participants, o => o.Ignore())
        //            .ForMember(e => e.Contacts, o => o.Ignore())
        //            .ForMember(e => e.Files, o => o.Ignore())
        //            .ForMember(e => e.RevisionId, o => o.Ignore())
        //            .ForMember(e => e.EntityId, o => o.Ignore())
        //            .ForMember(e => e.CreatedOnUtc, o => o.Ignore())
        //            .ForMember(e => e.CreatedByPrincipal, o => o.Ignore())
        //            .ForMember(e => e.UpdatedOnUtc, o => o.Ignore())
        //            .ForMember(e => e.UpdatedByPrincipal, o => o.Ignore())
        //            .ForMember(e => e.Version, o => o.Ignore())
        //            .ForMember(e => e.IsCurrent, o => o.Ignore())
        //            .ForMember(e => e.IsArchived, o => o.Ignore())
        //            .ForMember(e => e.IsDeleted, o => o.Ignore())
        //            .ForMember(e => e.Visibility, o => o.Ignore())
        //            .ForMember(e => e.VisibilityText, o => o.Ignore())
        //        ;
        //    }
        //}

        private class EntityToInstitutionalAgreementParticipantFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreementParticipant, InstitutionalAgreementParticipantForm>()
                    .ForMember(target => target.IsDeleted, o => o.Ignore())
                ;
            }
        }

        private class EntityToInstitutionalAgreementContactFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreementContact, InstitutionalAgreementContactForm>()
                    .ForMember(d => d.ContactType, o => o.MapFrom(s => s.Type))
                ;
                CreateMap<Person, InstitutionalAgreementContactForm.PersonForm>()
                    .ForMember(target => target.DefaultEmail, o => o
                        .ResolveUsing(source => source.Emails.SingleOrDefault(e => e.IsDefault)))
                ;

            }
        }

        private class EntityFromInstitutionalAgreementContactFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreementContactForm, InstitutionalAgreementContact>()
                    .ForMember(d => d.Type, o => o.MapFrom(s => s.ContactType))
                    .ForMember(e => e.Agreement, o => o.Ignore())
                    .ForMember(e => e.RevisionId, o => o.Ignore())
                    .ForMember(e => e.CreatedOnUtc, o => o.Ignore())
                    .ForMember(e => e.CreatedByPrincipal, o => o.Ignore())
                    .ForMember(e => e.UpdatedOnUtc, o => o.Ignore())
                    .ForMember(e => e.UpdatedByPrincipal, o => o.Ignore())
                    .ForMember(e => e.Version, o => o.Ignore())
                    .ForMember(e => e.IsCurrent, o => o.Ignore())
                    .ForMember(e => e.IsArchived, o => o.Ignore())
                    .ForMember(e => e.IsDeleted, o => o.Ignore())
                ;
                CreateMap<InstitutionalAgreementContactForm.PersonForm, Person>()
                    .ForMember(target => target.IsDisplayNameDerived, o => o.UseValue(true))
                    .ForMember(target => target.Emails, o => o
                        .ResolveUsing(source => source.DefaultEmail == null ? null :
                            new[]
                            {
                                new EmailAddress
                                {
                                    Value = source.DefaultEmail,
                                    IsDefault = true,
                                }
                            }))
                    .ForMember(e => e.RevisionId, o => o.Ignore())
                    .ForMember(e => e.User, o => o.Ignore())
                    .ForMember(e => e.Affiliations, o => o.Ignore())
                    .ForMember(e => e.Messages, o => o.Ignore())
                    .ForMember(e => e.CreatedOnUtc, o => o.Ignore())
                    .ForMember(e => e.CreatedByPrincipal, o => o.Ignore())
                    .ForMember(e => e.UpdatedOnUtc, o => o.Ignore())
                    .ForMember(e => e.UpdatedByPrincipal, o => o.Ignore())
                    .ForMember(e => e.Version, o => o.Ignore())
                    .ForMember(e => e.IsCurrent, o => o.Ignore())
                    .ForMember(e => e.IsArchived, o => o.Ignore())
                    .ForMember(e => e.IsDeleted, o => o.Ignore())
                ;
            }
        }

        private class EntityToInstitutionalAgreementFileFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreementFile, InstitutionalAgreementFileForm>()
                    .ForMember(m => m.PostedFile, o => o.Ignore())
                ;
            }
        }

        private class LooseFileIntoInstitutionalAgreementFileFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<LooseFile, InstitutionalAgreementFileForm>()
                    .ForMember(target => target.PostedFile, o => o.UseValue(null))
                ;
            }
        }

        //private class EntityToInstitutionalAgreementFileInfoProfile : Profile
        //{
        //    protected override void Configure()
        //    {
        //        CreateMap<InstitutionalAgreementFile, InstitutionalAgreementFileInfo>();
        //    }
        //}

        // ReSharper restore UnusedMember.Local
        // ReSharper restore ClassNeverInstantiated.Local
    }
}