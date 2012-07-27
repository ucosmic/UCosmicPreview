using AutoMapper;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Languages;
using UCosmic.Domain.Places;
using UCosmic.Www.Mvc.Areas.Establishments.Models.ManagementForms;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Establishments.Mappers
{
    public static class ManagementFormsModelMapper
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(ManagementFormsModelMapper));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToEstablishmentFormProfile : Profile
        {
            protected override void Configure()
            {
                // convert entities to models
                CreateMap<Establishment, EstablishmentForm>()
                    // these do not map to the entity
                    .ForMember(target => target.ReturnUrl, opt => opt.Ignore())

                    // only map non-official names
                    .ForMember(target => target.Names, opt => opt
                        .ResolveUsing(source => source.Names.WhereIsNotOfficialName()))
                ;
                CreateMap<EstablishmentLocation, EstablishmentForm.LocationForm>()
                ;
                CreateMap<EstablishmentType, EstablishmentForm.EstablishmentTypeForm>()
                    // these do not map to the entity
                    .ForMember(target => target.Options, opt => opt.Ignore())
                ;
                CreateMap<Place, EstablishmentForm.LocationForm.EstablishmentPlaceForm>()
                    .ForMember(target => target.PlaceType, opt => opt.ResolveUsing(source =>
                    {
                        if (source.IsEarth) return null;
                        if (source.IsCountry) return source.GeoPlanetPlace.Country.TypeName ?? source.GeoPlanetPlace.Type.EnglishName;
                        if (source.IsAdmin1) return source.GeoPlanetPlace.Admin1.TypeName ?? source.GeoPlanetPlace.Type.EnglishName;
                        if (source.IsAdmin2) return source.GeoPlanetPlace.Admin2.TypeName ?? source.GeoPlanetPlace.Type.EnglishName;
                        if (source.IsAdmin3) return source.GeoPlanetPlace.Admin3.TypeName ?? source.GeoPlanetPlace.Type.EnglishName;
                        return source.GeoPlanetPlace.Type.EnglishName;
                    })
                    )
                ;
                CreateMap<EstablishmentName, EstablishmentForm.EstablishmentNameForm>()
                    // ensure there is a TranslationToLanguage model even when null
                    .ForMember(target => target.TranslationToLanguage, opt => opt
                        .ResolveUsing(source => (source.TranslationToLanguage != null)
                            ? Mapper.Map<EstablishmentForm.EstablishmentNameForm.LanguageForm>(source.TranslationToLanguage)
                            : new EstablishmentForm.EstablishmentNameForm.LanguageForm()))

                    // any name in the entity has already been added to the form
                    .ForMember(f => f.IsAdded, opt => opt.MapFrom(v => true))
                ;
                CreateMap<Language, EstablishmentForm.EstablishmentNameForm.LanguageForm>()
                    // these do not map to the entity
                    .ForMember(target => target.Options, opt => opt.Ignore())
                ;
            }
        }

        private class EntityFromEstablishmentFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<EstablishmentForm, Establishment>()
                    .ForMember(target => target.CreatedOnUtc, opt => opt.Ignore())
                    .ForMember(target => target.CreatedByPrincipal, opt => opt.Ignore())
                    .ForMember(target => target.UpdatedOnUtc, opt => opt.Ignore())
                    .ForMember(target => target.UpdatedByPrincipal, opt => opt.Ignore())
                    .ForMember(target => target.Version, opt => opt.Ignore())
                    .ForMember(target => target.IsCurrent, opt => opt.Ignore())
                    .ForMember(target => target.IsArchived, opt => opt.Ignore())
                    .ForMember(target => target.IsDeleted, opt => opt.Ignore())
                    .ForMember(target => target.Parent, opt => opt.Ignore())
                    .ForMember(target => target.Ancestors, opt => opt.Ignore())
                    .ForMember(target => target.Children, opt => opt.Ignore())
                    .ForMember(target => target.Offspring, opt => opt.Ignore())
                    .ForMember(target => target.InstitutionInfo, opt => opt.Ignore())
                    .ForMember(target => target.PublicContactInfo, opt => opt.Ignore())
                    .ForMember(target => target.PartnerContactInfo, opt => opt.Ignore())
                    .ForMember(target => target.SamlSignOn, opt => opt.Ignore())
                    .ForMember(target => target.Affiliates, opt => opt.Ignore())
                    .ForMember(target => target.Urls, opt => opt.Ignore())
                    .ForMember(target => target.EmailDomains, opt => opt.Ignore())
                ;
                CreateMap<EstablishmentForm.EstablishmentNameForm, EstablishmentName>()
                    .ForMember(target => target.RevisionId, opt => opt.ResolveUsing(s => s.RevisionId))
                    .ForMember(target => target.EntityId, opt => opt.Ignore())
                    .ForMember(target => target.CreatedOnUtc, opt => opt.Ignore())
                    .ForMember(target => target.CreatedByPrincipal, opt => opt.Ignore())
                    .ForMember(target => target.UpdatedOnUtc, opt => opt.Ignore())
                    .ForMember(target => target.UpdatedByPrincipal, opt => opt.Ignore())
                    .ForMember(target => target.Version, opt => opt.Ignore())
                    .ForMember(target => target.IsCurrent, opt => opt.Ignore())
                    .ForMember(target => target.IsArchived, opt => opt.Ignore())
                    .ForMember(target => target.IsDeleted, opt => opt.Ignore())
                    .ForMember(target => target.IsOfficialName, opt => opt.Ignore())
                    .ForMember(target => target.AsciiEquivalent, opt => opt.Ignore())
                    .ForMember(target => target.TranslationToHint, opt => opt.Ignore())
                    .ForMember(target => target.ForEstablishment, opt => opt.Ignore())
                ;
                CreateMap<EstablishmentForm.EstablishmentNameForm.LanguageForm, Language>()
                    .ForMember(target => target.EntityId, opt => opt.Ignore())
                    .ForMember(target => target.CreatedOnUtc, opt => opt.Ignore())
                    .ForMember(target => target.CreatedByPrincipal, opt => opt.Ignore())
                    .ForMember(target => target.UpdatedOnUtc, opt => opt.Ignore())
                    .ForMember(target => target.UpdatedByPrincipal, opt => opt.Ignore())
                    .ForMember(target => target.Version, opt => opt.Ignore())
                    .ForMember(target => target.IsCurrent, opt => opt.Ignore())
                    .ForMember(target => target.IsArchived, opt => opt.Ignore())
                    .ForMember(target => target.IsDeleted, opt => opt.Ignore())
                    .ForMember(target => target.TwoLetterIsoCode, opt => opt.Ignore())
                    .ForMember(target => target.ThreeLetterIsoCode, opt => opt.Ignore())
                    .ForMember(target => target.ThreeLetterIsoBibliographicCode, opt => opt.Ignore())
                    .ForMember(target => target.Names, opt => opt.Ignore())
                ;
                CreateMap<EstablishmentForm.LocationForm, EstablishmentLocation>()
                    .ForMember(target => target.RevisionId, opt => opt.Ignore())
                    .ForMember(target => target.EntityId, opt => opt.Ignore())
                    .ForMember(target => target.CreatedOnUtc, opt => opt.Ignore())
                    .ForMember(target => target.CreatedByPrincipal, opt => opt.Ignore())
                    .ForMember(target => target.UpdatedOnUtc, opt => opt.Ignore())
                    .ForMember(target => target.UpdatedByPrincipal, opt => opt.Ignore())
                    .ForMember(target => target.Version, opt => opt.Ignore())
                    .ForMember(target => target.IsCurrent, opt => opt.Ignore())
                    .ForMember(target => target.IsArchived, opt => opt.Ignore())
                    .ForMember(target => target.IsDeleted, opt => opt.Ignore())
                    .ForMember(target => target.ForEstablishment, opt => opt.Ignore())
                    .ForMember(target => target.Addresses, opt => opt.Ignore())
                    .ForMember(target => target.Center, opt => opt.ResolveUsing(source =>
                        new Coordinates { Latitude = source.CenterLatitude, Longitude = source.CenterLongitude }))
                    .ForMember(target => target.BoundingBox, opt => opt.ResolveUsing(source =>
                        new BoundingBox
                        {
                            Northeast = new Coordinates
                            {
                                Latitude = source.BoundingBoxNortheastLatitude,
                                Longitude = source.BoundingBoxNortheastLongitude,
                            },
                            Southwest = new Coordinates
                            {
                                Latitude = source.BoundingBoxSouthwestLatitude,
                                Longitude = source.BoundingBoxSouthwestLongitude,
                            },
                        }))
                ;
                CreateMap<EstablishmentForm.LocationForm.EstablishmentPlaceForm, Place>()
                    .ForMember(target => target.EntityId, opt => opt.Ignore())
                    .ForMember(target => target.CreatedOnUtc, opt => opt.Ignore())
                    .ForMember(target => target.CreatedByPrincipal, opt => opt.Ignore())
                    .ForMember(target => target.UpdatedOnUtc, opt => opt.Ignore())
                    .ForMember(target => target.UpdatedByPrincipal, opt => opt.Ignore())
                    .ForMember(target => target.Version, opt => opt.Ignore())
                    .ForMember(target => target.IsCurrent, opt => opt.Ignore())
                    .ForMember(target => target.IsArchived, opt => opt.Ignore())
                    .ForMember(target => target.Parent, opt => opt.Ignore())
                    .ForMember(target => target.Children, opt => opt.Ignore())
                    .ForMember(target => target.Ancestors, opt => opt.Ignore())
                    .ForMember(target => target.Offspring, opt => opt.Ignore())
                    .ForMember(target => target.IsContinent, opt => opt.Ignore())
                    .ForMember(target => target.IsCountry, opt => opt.Ignore())
                    .ForMember(target => target.IsAdmin1, opt => opt.Ignore())
                    .ForMember(target => target.IsAdmin2, opt => opt.Ignore())
                    .ForMember(target => target.IsAdmin3, opt => opt.Ignore())
                    .ForMember(target => target.Center, opt => opt.Ignore())
                    .ForMember(target => target.BoundingBox, opt => opt.Ignore())
                    .ForMember(target => target.GeoNamesToponym, opt => opt.Ignore())
                    .ForMember(target => target.GeoPlanetPlace, opt => opt.Ignore())
                    .ForMember(target => target.Names, opt => opt.Ignore())
                ;
                CreateMap<EstablishmentForm.EstablishmentTypeForm, EstablishmentType>()
                    .ForMember(target => target.EntityId, opt => opt.Ignore())
                    .ForMember(target => target.CreatedOnUtc, opt => opt.Ignore())
                    .ForMember(target => target.CreatedByPrincipal, opt => opt.Ignore())
                    .ForMember(target => target.UpdatedOnUtc, opt => opt.Ignore())
                    .ForMember(target => target.UpdatedByPrincipal, opt => opt.Ignore())
                    .ForMember(target => target.Version, opt => opt.Ignore())
                    .ForMember(target => target.IsCurrent, opt => opt.Ignore())
                    .ForMember(target => target.IsArchived, opt => opt.Ignore())
                    .ForMember(target => target.IsDeleted, opt => opt.Ignore())
                    .ForMember(target => target.EnglishName, opt => opt.Ignore())
                    .ForMember(target => target.EnglishPluralName, opt => opt.Ignore())
                    .ForMember(target => target.CategoryId, opt => opt.Ignore())
                    .ForMember(target => target.Category, opt => opt.Ignore())
                ;
            }
        }

        private class EstablishmentSearchResultProfile : Profile
        {
            protected override void Configure()
            {
                // convert entity to model
                CreateMap<Establishment, EstablishmentSearchResult>();
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}