using System.Linq;
using AutoMapper;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Domain.People;
using UCosmic.Domain.Places;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.PublicSearch;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Mappers
{
    public static class PublicSearchModelMapper
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(PublicSearchModelMapper));
        }

        // ReSharper disable UnusedMember.Local

        private class EntitiesToSearchResultsProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Establishment, SearchResults.EstablishmentInfo>();
                CreateMap<EstablishmentLocation, SearchResults.EstablishmentInfo.LocationInfo>();
                CreateMap<InstitutionalAgreement, SearchResults.AgreementInfo>()
                    .ForMember(target => target.Partners, opt => opt
                        .ResolveUsing(source => source.Participants.Where(p => !p.IsOwner).Select(p => p.Establishment)))
                ;
            }
        }

        private class EntityToAgreementInfoProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreement, AgreementInfo>()
                    .ForMember(target => target.Partners, opt => opt
                        .ResolveUsing(source => source.Participants.Where(p => !p.IsOwner).Select(p => p.Establishment)))
                    .ForMember(target => target.Owners, opt => opt
                        .ResolveUsing(source => source.Participants.Where(p => p.IsOwner).Select(p => p.Establishment)))
                    .ForMember(target => target.IsManager, opt => opt.UseValue(false))
                    .ForMember(target => target.IsAffiliate, opt => opt.UseValue(false))
                    .ForMember(target => target.ReturnUrl, opt => opt.Ignore())
                    .ForMember(target => target.DistinctEmailDomains, opt => opt.Ignore())
                ;
                CreateMap<Establishment, AgreementInfo.EstablishmentInfo>();
                CreateMap<EstablishmentLocation, AgreementInfo.EstablishmentInfo.LocationInfo>()
                    .ForMember(target => target.DistinctPlaces, opt => opt.Ignore())
                    .ForMember(target => target.Places, opt => opt.ResolveUsing(source => source.Places.OrderBy(p => p.Ancestors.Count)))
                ;
                CreateMap<Place, AgreementInfo.EstablishmentInfo.LocationInfo.PlaceInfo>();
                CreateMap<EstablishmentEmailDomain, AgreementInfo.EstablishmentInfo.EmailDomainInfo>();
                CreateMap<InstitutionalAgreementFile, AgreementInfo.FileInfo>();
                CreateMap<InstitutionalAgreementContact, AgreementInfo.ContactInfo>();
                CreateMap<Person, AgreementInfo.ContactInfo.PersonInfo>()
                    .ForMember(target => target.DefaultEmail, opt => opt
                        .ResolveUsing(source => source.Emails.SingleOrDefault(e => e.IsDefault)))
                ;
            }
        }

        private class EntitiesToEstablishmentInfoProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Establishment, EstablishmentInfo>();
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}