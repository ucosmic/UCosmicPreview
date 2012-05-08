using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UCosmic.Domain;
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
                    .ForMember(dto => dto.Files, opt => opt.ResolveUsing<InstitutionalAgreementToSearchResultFilesResolver>());
            }

            private class InstitutionalAgreementToSearchResultFilesResolver : ValueResolver<InstitutionalAgreement, IList<InstitutionalAgreementFileInfo>>
            {
                protected override IList<InstitutionalAgreementFileInfo> ResolveCore(InstitutionalAgreement source)
                {
                    if (source.Files == null) return null;

                    var currentFiles = source.Files.Current();
                    var currentFileInfos = Mapper.Map<IList<InstitutionalAgreementFileInfo>>(currentFiles);
                    return currentFileInfos;
                }
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
                    .ForMember(t => t.Agreements, opt => opt.Ignore())
                ;
            }
        }

        private class EntityToInstitutionalAgreementFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreement, InstitutionalAgreementForm>()
                    .ForMember(target => target.ReturnUrl, opt => opt.Ignore())
                    .ForMember(target => target.Files, opt => opt.ResolveUsing<InstitutionalAgreementToFormFilesResolver>())
                    .ForMember(target => target.Contacts, opt => opt.ResolveUsing<InstitutionalAgreementToFormContactsResolver>())
                    .ForMember(target => target.Umbrella, opt => opt
                        .ResolveUsing(source => source.Umbrella != null 
                            ? Mapper.Map<InstitutionalAgreementForm.UmbrellaForm>(source.Umbrella) 
                            : new InstitutionalAgreementForm.UmbrellaForm()))
                ;
                CreateMap<InstitutionalAgreement, InstitutionalAgreementForm.UmbrellaForm>()
                    .ForMember(target => target.Options, opt => opt.Ignore())
                ;
            }

            private class InstitutionalAgreementToFormFilesResolver : ValueResolver<InstitutionalAgreement, IList<InstitutionalAgreementFileForm>>
            {
                protected override IList<InstitutionalAgreementFileForm> ResolveCore(InstitutionalAgreement source)
                {
                    if (source.Files == null) return null;

                    var currentEntities = source.Files.Current();
                    var currentModels = Mapper.Map<IList<InstitutionalAgreementFileForm>>(currentEntities);
                    return currentModels;
                }
            }

            private class InstitutionalAgreementToFormContactsResolver : ValueResolver<InstitutionalAgreement, IList<InstitutionalAgreementContactForm>>
            {
                protected override IList<InstitutionalAgreementContactForm> ResolveCore(InstitutionalAgreement source)
                {
                    if (source.Contacts == null) return null;

                    var currentEntities = source.Contacts.Current();
                    var currentModels = Mapper.Map<IList<InstitutionalAgreementContactForm>>(currentEntities);
                    return currentModels;
                }
            }
        }

        private class EntityFromInstitutionalAgreementFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreementForm, InstitutionalAgreement>()
                    .ForMember(e => e.Umbrella, opt => opt.Ignore())
                    .ForMember(e => e.Ancestors, opt => opt.Ignore())
                    .ForMember(e => e.Children, opt => opt.Ignore())
                    .ForMember(e => e.Offspring, opt => opt.Ignore())
                    .ForMember(e => e.Contacts, opt => opt.Ignore())
                    .ForMember(e => e.Participants, opt => opt.Ignore())
                    .ForMember(e => e.Files, opt => opt.Ignore())
                    .ForMember(e => e.CreatedOnUtc, opt => opt.Ignore())
                    .ForMember(e => e.CreatedByPrincipal, opt => opt.Ignore())
                    .ForMember(e => e.UpdatedOnUtc, opt => opt.Ignore())
                    .ForMember(e => e.UpdatedByPrincipal, opt => opt.Ignore())
                    .ForMember(e => e.Version, opt => opt.Ignore())
                    .ForMember(e => e.IsCurrent, opt => opt.Ignore())
                    .ForMember(e => e.IsArchived, opt => opt.Ignore())
                    .ForMember(e => e.IsDeleted, opt => opt.Ignore())
                ;
            }
        }

        private class EntityToInstitutionalAgreementUmbrellaOptionProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreement, InstitutionalAgreementUmbrellaOption>();
            }
        }

        private class EntityFromInstitutionalAgreementDeriveTitleInputProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreementDeriveTitleInput, InstitutionalAgreement>()
                    .ForMember(e => e.Umbrella, opt => opt.Ignore())
                    .ForMember(e => e.Ancestors, opt => opt.Ignore())
                    .ForMember(e => e.Children, opt => opt.Ignore())
                    .ForMember(e => e.Offspring, opt => opt.Ignore())
                    .ForMember(e => e.StartsOn, opt => opt.Ignore())
                    .ForMember(e => e.ExpiresOn, opt => opt.Ignore())
                    .ForMember(e => e.IsExpirationEstimated, opt => opt.Ignore())
                    .ForMember(e => e.Description, opt => opt.Ignore())
                    .ForMember(e => e.IsAutoRenew, opt => opt.Ignore())
                    .ForMember(e => e.Participants, opt => opt.Ignore())
                    .ForMember(e => e.Contacts, opt => opt.Ignore())
                    .ForMember(e => e.Files, opt => opt.Ignore())
                    .ForMember(e => e.RevisionId, opt => opt.Ignore())
                    .ForMember(e => e.EntityId, opt => opt.Ignore())
                    .ForMember(e => e.CreatedOnUtc, opt => opt.Ignore())
                    .ForMember(e => e.CreatedByPrincipal, opt => opt.Ignore())
                    .ForMember(e => e.UpdatedOnUtc, opt => opt.Ignore())
                    .ForMember(e => e.UpdatedByPrincipal, opt => opt.Ignore())
                    .ForMember(e => e.Version, opt => opt.Ignore())
                    .ForMember(e => e.IsCurrent, opt => opt.Ignore())
                    .ForMember(e => e.IsArchived, opt => opt.Ignore())
                    .ForMember(e => e.IsDeleted, opt => opt.Ignore())
                ;
            }
        }

        private class EntityToInstitutionalAgreementParticipantFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreementParticipant, InstitutionalAgreementParticipantForm>()
                    .ForMember(target => target.IsDeleted, opt => opt.Ignore())
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
                    .ForMember(target => target.DefaultEmail, opt => opt
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
                    .ForMember(e => e.Agreement, opt => opt.Ignore())
                    .ForMember(e => e.RevisionId, opt => opt.Ignore())
                    .ForMember(e => e.CreatedOnUtc, opt => opt.Ignore())
                    .ForMember(e => e.CreatedByPrincipal, opt => opt.Ignore())
                    .ForMember(e => e.UpdatedOnUtc, opt => opt.Ignore())
                    .ForMember(e => e.UpdatedByPrincipal, opt => opt.Ignore())
                    .ForMember(e => e.Version, opt => opt.Ignore())
                    .ForMember(e => e.IsCurrent, opt => opt.Ignore())
                    .ForMember(e => e.IsArchived, opt => opt.Ignore())
                    .ForMember(e => e.IsDeleted, opt => opt.Ignore())
                ;
                CreateMap<InstitutionalAgreementContactForm.PersonForm, Person>()
                    .ForMember(target => target.IsDisplayNameDerived, opt => opt.UseValue(true))
                    .ForMember(target => target.Emails, opt => opt
                        .ResolveUsing(source => new[]
                            {
                                new EmailAddress
                                {
                                    Value = source.DefaultEmail,
                                    IsDefault = true,
                                }
                            }))
                    .ForMember(e => e.RevisionId, opt => opt.Ignore())
                    .ForMember(e => e.User, opt => opt.Ignore())
                    .ForMember(e => e.Affiliations, opt => opt.Ignore())
                    .ForMember(e => e.Messages, opt => opt.Ignore())
                    .ForMember(e => e.CreatedOnUtc, opt => opt.Ignore())
                    .ForMember(e => e.CreatedByPrincipal, opt => opt.Ignore())
                    .ForMember(e => e.UpdatedOnUtc, opt => opt.Ignore())
                    .ForMember(e => e.UpdatedByPrincipal, opt => opt.Ignore())
                    .ForMember(e => e.Version, opt => opt.Ignore())
                    .ForMember(e => e.IsCurrent, opt => opt.Ignore())
                    .ForMember(e => e.IsArchived, opt => opt.Ignore())
                    .ForMember(e => e.IsDeleted, opt => opt.Ignore())
                ;
            }
        }

        private class EntityToInstitutionalAgreementFileFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreementFile, InstitutionalAgreementFileForm>()
                    .ForMember(m => m.PostedFile, opt => opt.Ignore())
                ;
            }
        }

        private class LooseFileIntoInstitutionalAgreementFileFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<LooseFile, InstitutionalAgreementFileForm>()
                    .ForMember(target => target.PostedFile, opt => opt.UseValue(null))
                ;
            }
        }

        private class EntityToInstitutionalAgreementFileInfoProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreementFile, InstitutionalAgreementFileInfo>();
            }
        }

        // ReSharper restore UnusedMember.Local
        // ReSharper restore ClassNeverInstantiated.Local
    }
}