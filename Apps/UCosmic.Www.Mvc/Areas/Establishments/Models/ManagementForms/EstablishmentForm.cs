using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Models;

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
                public int? RevisionId { get; set; }
                public string TranslatedNameText { get; set; }

                [ScaffoldColumn(false)]
                public SelectListItem[] Options { get; set; }
            }

        }
    }
}
