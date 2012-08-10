using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Languages;
using UCosmic.Domain.Places;
using UCosmic.Www.Mvc.Models;
using AutoMapper;

namespace UCosmic.Www.Mvc.Areas.Establishments.Models.ManagementForms
{
    public class EstablishmentForm
    {
        public EstablishmentForm()
        {
            Type = new EstablishmentTypeForm();
            Names = new List<EstablishmentNameForm>();
            Location = new LocationForm();
        }

        [ScaffoldColumn(false)]
        public bool IsNew { get { return RevisionId == 0; } }

        [HiddenInput(DisplayValue = false)]
        public int RevisionId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid EntityId { get; set; }

        [Display(Name = "Official Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "{0} is required.")]
        public string OfficialName { get; set; }
        public string TranslatedName { get; set; }
        public bool HasTranslatedName
        {
            get
            {
                return !string.IsNullOrWhiteSpace(TranslatedName)
                    && !TranslatedName.Equals(OfficialName, StringComparison.OrdinalIgnoreCase);
            }
        }
        public string OfficialThenTranslatedName
        {
            get
            {
                var name = OfficialName;
                if (HasTranslatedName)
                    name = string.Format("{0} ({1})", name, TranslatedName);
                return name;
            }
        }
        public string TranslatedThenOfficialName
        {
            get
            {
                var name = TranslatedName;
                name = HasTranslatedName ? string.Format("{0} ({1})", name, OfficialName) : OfficialName;
                return name;
            }
        }

        [Display(Name = "Website Url")]
        [DataType(DataType.Text)]
        public string WebsiteUrl { get; set; }

        [Display(Name = "Is a Member")]
        public bool IsMember { get; set; }

        public string ReturnUrl { get; set; }

        public EstablishmentTypeForm Type { get; set; }
        public class EstablishmentTypeForm
        {
            [Required(ErrorMessage = "Establishment type is required 2.")]
            [DisplayFormat(NullDisplayText = "[Select an establishment type]")]
            [Display(Name = "Type")]
            public int? RevisionId { get; set; }

            public GroupedSelectListItem[] Options { get; set; }
        }

        public LocationForm Location { get; set; }
        public class LocationForm
        {
            public int? GoogleMapZoomLevel { get; set; }
            public double? CenterLatitude { get; set; }
            public double? CenterLongitude { get; set; }
            public double? BoundingBoxNortheastLatitude { get; set; }
            public double? BoundingBoxNortheastLongitude { get; set; }
            public double? BoundingBoxSouthwestLatitude { get; set; }
            public double? BoundingBoxSouthwestLongitude { get; set; }

            public bool HasPlaceMark
            {
                get { return CenterLatitude.HasValue && CenterLongitude.HasValue; }
            }

            public EstablishmentPlacesForm Places { get; set; }
            public class EstablishmentPlacesForm : Collection<EstablishmentPlaceForm>
            {
            }
            public class EstablishmentPlaceForm
            {
                [HiddenInput(DisplayValue = false)]
                public int RevisionId { get; set; }

                [HiddenInput(DisplayValue = false)]
                public string OfficialName { get; set; }

                [HiddenInput(DisplayValue = false)]
                public string PlaceType { get; set; }

                public bool IsEarth { get; set; }

                [HiddenInput(DisplayValue = false)]
                public bool IsDeleted { get; set; }

                public override string ToString()
                {
                    return string.Format(CultureInfo.CurrentUICulture, "{0} ({1})", OfficialName, PlaceType);
                }
            }
        }

        public IList<EstablishmentNameForm> Names { get; set; }
        public class EstablishmentNameForm
        {
            public EstablishmentNameForm(SelectListItem[] languageOptions)
            {
                TranslationToLanguage = new LanguageForm
                {
                    Options = languageOptions,
                };
            }
            public EstablishmentNameForm() : this(null)
            {
            }

            [HiddenInput(DisplayValue = false)]
            public int RevisionId { get; set; }

            [HiddenInput(DisplayValue = false)]
            public int ForEstablishmentRevisionId { get; set; }

            [Display(Name = "Establishment Name")]
            [RequiredIfClient("IsAdded", ComparisonType.IsEqualTo, true, ErrorMessage = "{0} is required.")]
            [StringLength(500, ErrorMessage = "{0} cannot contain more than {1} characters.")]
            public string Text { get; set; }

            [DefaultValue(false)]
            public bool IsFormerName { get; set; }

            [HiddenInput(DisplayValue = false)]
            public bool IsAdded { get; set; }

            [DisplayFormat(NullDisplayText = "[Unknown language]")]
            [Display(Name = "Language")]
            public LanguageForm TranslationToLanguage { get; set; }
            public class LanguageForm
            {
                public int? Id { get; set; }
                public string TranslatedNameText { get; set; }

                [ScaffoldColumn(false)]
                public SelectListItem[] Options { get; set; }
            }
        }
    }

    public static class EstablishmentFormProfiler
    {
        public class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                // convert entities to models
                CreateMap<Establishment, EstablishmentForm>()
                    // these do not map to the entity
                    .ForMember(target => target.ReturnUrl, opt => opt.Ignore())

                    // only map non-official names
                    .ForMember(target => target.Names, opt => opt
                        .ResolveUsing(source => source.Names.Where(n => !n.IsOfficialName)))
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

        //public class ModelToEntityProfile : Profile
        //{
        //    protected override void Configure()
        //    {
        //        CreateMap<EstablishmentForm, Establishment>()
        //            .ForMember(target => target.CreatedOnUtc, opt => opt.Ignore())
        //            .ForMember(target => target.CreatedByPrincipal, opt => opt.Ignore())
        //            .ForMember(target => target.UpdatedOnUtc, opt => opt.Ignore())
        //            .ForMember(target => target.UpdatedByPrincipal, opt => opt.Ignore())
        //            .ForMember(target => target.Version, opt => opt.Ignore())
        //            .ForMember(target => target.IsCurrent, opt => opt.Ignore())
        //            .ForMember(target => target.IsArchived, opt => opt.Ignore())
        //            .ForMember(target => target.IsDeleted, opt => opt.Ignore())
        //            .ForMember(target => target.Parent, opt => opt.Ignore())
        //            .ForMember(target => target.Ancestors, opt => opt.Ignore())
        //            .ForMember(target => target.Children, opt => opt.Ignore())
        //            .ForMember(target => target.Offspring, opt => opt.Ignore())
        //            .ForMember(target => target.InstitutionInfo, opt => opt.Ignore())
        //            .ForMember(target => target.PublicContactInfo, opt => opt.Ignore())
        //            .ForMember(target => target.PartnerContactInfo, opt => opt.Ignore())
        //            .ForMember(target => target.SamlSignOn, opt => opt.Ignore())
        //            .ForMember(target => target.Affiliates, opt => opt.Ignore())
        //            .ForMember(target => target.Urls, opt => opt.Ignore())
        //            .ForMember(target => target.EmailDomains, opt => opt.Ignore())
        //        ;
        //        CreateMap<EstablishmentForm.EstablishmentNameForm, EstablishmentName>()
        //            .ForMember(target => target.RevisionId, opt => opt.ResolveUsing(s => s.RevisionId))
        //            .ForMember(target => target.EntityId, opt => opt.Ignore())
        //            .ForMember(target => target.CreatedOnUtc, opt => opt.Ignore())
        //            .ForMember(target => target.CreatedByPrincipal, opt => opt.Ignore())
        //            .ForMember(target => target.UpdatedOnUtc, opt => opt.Ignore())
        //            .ForMember(target => target.UpdatedByPrincipal, opt => opt.Ignore())
        //            .ForMember(target => target.Version, opt => opt.Ignore())
        //            .ForMember(target => target.IsCurrent, opt => opt.Ignore())
        //            .ForMember(target => target.IsArchived, opt => opt.Ignore())
        //            .ForMember(target => target.IsDeleted, opt => opt.Ignore())
        //            .ForMember(target => target.IsOfficialName, opt => opt.Ignore())
        //            .ForMember(target => target.AsciiEquivalent, opt => opt.Ignore())
        //            .ForMember(target => target.ForEstablishment, opt => opt.Ignore())
        //        ;
        //        CreateMap<EstablishmentForm.EstablishmentNameForm.LanguageForm, Language>()
        //            .ForMember(target => target.EntityId, opt => opt.Ignore())
        //            .ForMember(target => target.CreatedOnUtc, opt => opt.Ignore())
        //            .ForMember(target => target.CreatedByPrincipal, opt => opt.Ignore())
        //            .ForMember(target => target.UpdatedOnUtc, opt => opt.Ignore())
        //            .ForMember(target => target.UpdatedByPrincipal, opt => opt.Ignore())
        //            .ForMember(target => target.Version, opt => opt.Ignore())
        //            .ForMember(target => target.IsCurrent, opt => opt.Ignore())
        //            .ForMember(target => target.IsArchived, opt => opt.Ignore())
        //            .ForMember(target => target.IsDeleted, opt => opt.Ignore())
        //            .ForMember(target => target.TwoLetterIsoCode, opt => opt.Ignore())
        //            .ForMember(target => target.ThreeLetterIsoCode, opt => opt.Ignore())
        //            .ForMember(target => target.ThreeLetterIsoBibliographicCode, opt => opt.Ignore())
        //            .ForMember(target => target.Names, opt => opt.Ignore())
        //        ;
        //        CreateMap<EstablishmentForm.LocationForm, EstablishmentLocation>()
        //            .ForMember(target => target.RevisionId, opt => opt.Ignore())
        //            .ForMember(target => target.EntityId, opt => opt.Ignore())
        //            .ForMember(target => target.CreatedOnUtc, opt => opt.Ignore())
        //            .ForMember(target => target.CreatedByPrincipal, opt => opt.Ignore())
        //            .ForMember(target => target.UpdatedOnUtc, opt => opt.Ignore())
        //            .ForMember(target => target.UpdatedByPrincipal, opt => opt.Ignore())
        //            .ForMember(target => target.Version, opt => opt.Ignore())
        //            .ForMember(target => target.IsCurrent, opt => opt.Ignore())
        //            .ForMember(target => target.IsArchived, opt => opt.Ignore())
        //            .ForMember(target => target.IsDeleted, opt => opt.Ignore())
        //            .ForMember(target => target.ForEstablishment, opt => opt.Ignore())
        //            .ForMember(target => target.Addresses, opt => opt.Ignore())
        //            .ForMember(target => target.Center, opt => opt.ResolveUsing(source =>
        //                new Coordinates(source.CenterLatitude, source.CenterLongitude)))
        //            .ForMember(target => target.BoundingBox, opt => opt.ResolveUsing(source =>
        //                new BoundingBox
        //                {
        //                    Northeast = new Coordinates
        //                    (
        //                        source.BoundingBoxNortheastLatitude,
        //                        source.BoundingBoxNortheastLongitude
        //                    ),
        //                    Southwest = new Coordinates
        //                    (
        //                        source.BoundingBoxSouthwestLatitude,
        //                        source.BoundingBoxSouthwestLongitude
        //                    ),
        //                }))
        //        ;
        //        CreateMap<EstablishmentForm.LocationForm.EstablishmentPlaceForm, Place>()
        //            .ForMember(target => target.EntityId, opt => opt.Ignore())
        //            .ForMember(target => target.CreatedOnUtc, opt => opt.Ignore())
        //            .ForMember(target => target.CreatedByPrincipal, opt => opt.Ignore())
        //            .ForMember(target => target.UpdatedOnUtc, opt => opt.Ignore())
        //            .ForMember(target => target.UpdatedByPrincipal, opt => opt.Ignore())
        //            .ForMember(target => target.Version, opt => opt.Ignore())
        //            .ForMember(target => target.IsCurrent, opt => opt.Ignore())
        //            .ForMember(target => target.IsArchived, opt => opt.Ignore())
        //            .ForMember(target => target.Parent, opt => opt.Ignore())
        //            .ForMember(target => target.Children, opt => opt.Ignore())
        //            .ForMember(target => target.Ancestors, opt => opt.Ignore())
        //            .ForMember(target => target.Offspring, opt => opt.Ignore())
        //            .ForMember(target => target.IsContinent, opt => opt.Ignore())
        //            .ForMember(target => target.IsCountry, opt => opt.Ignore())
        //            .ForMember(target => target.IsAdmin1, opt => opt.Ignore())
        //            .ForMember(target => target.IsAdmin2, opt => opt.Ignore())
        //            .ForMember(target => target.IsAdmin3, opt => opt.Ignore())
        //            .ForMember(target => target.Center, opt => opt.Ignore())
        //            .ForMember(target => target.BoundingBox, opt => opt.Ignore())
        //            .ForMember(target => target.GeoNamesToponym, opt => opt.Ignore())
        //            .ForMember(target => target.GeoPlanetPlace, opt => opt.Ignore())
        //            .ForMember(target => target.Names, opt => opt.Ignore())
        //        ;
        //        CreateMap<EstablishmentForm.EstablishmentTypeForm, EstablishmentType>()
        //            .ForMember(target => target.EntityId, opt => opt.Ignore())
        //            .ForMember(target => target.CreatedOnUtc, opt => opt.Ignore())
        //            .ForMember(target => target.CreatedByPrincipal, opt => opt.Ignore())
        //            .ForMember(target => target.UpdatedOnUtc, opt => opt.Ignore())
        //            .ForMember(target => target.UpdatedByPrincipal, opt => opt.Ignore())
        //            .ForMember(target => target.Version, opt => opt.Ignore())
        //            .ForMember(target => target.IsCurrent, opt => opt.Ignore())
        //            .ForMember(target => target.IsArchived, opt => opt.Ignore())
        //            .ForMember(target => target.IsDeleted, opt => opt.Ignore())
        //            .ForMember(target => target.EnglishName, opt => opt.Ignore())
        //            .ForMember(target => target.EnglishPluralName, opt => opt.Ignore())
        //            .ForMember(target => target.CategoryCode, opt => opt.Ignore())
        //            .ForMember(target => target.Category, opt => opt.Ignore())
        //        ;
        //    }
        //}
    }
}
