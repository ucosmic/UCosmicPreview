using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using UCosmic.Domain.Places;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.PublicSearch
{
    public class AgreementInfo : IReturnUrl
    {
        public bool IsManager { get; set; }
        public bool IsAffiliate { get; set; }
        public string ReturnUrl { get; set; }

        public IEnumerable<string> DistinctEmailDomains { get; set; }

        private BoundingBox _boundingBox;
        public BoundingBox MapBoundingBox
        {
            get
            {
                if (_boundingBox == null)
                {
                    foreach (var partner in Partners.Where(p => p.Location.BoundingBox.HasValue))
                    {
                        if (_boundingBox == null)
                        {
                            _boundingBox = partner.Location.BoundingBox;
                            continue;
                        }
                        // ReSharper disable PossibleInvalidOperationException

                        // northern latitudes are positive, southern latitudes are negative
                        _boundingBox.Northeast.Latitude = Math.Max(_boundingBox.Northeast.Latitude.Value, partner.Location.BoundingBox.Northeast.Latitude.Value);
                        _boundingBox.Southwest.Latitude = Math.Min(_boundingBox.Southwest.Latitude.Value, partner.Location.BoundingBox.Southwest.Latitude.Value);

                        _boundingBox.Northeast.Longitude = Math.Max(_boundingBox.Northeast.Longitude.Value, partner.Location.BoundingBox.Northeast.Longitude.Value);
                        _boundingBox.Southwest.Longitude = Math.Min(_boundingBox.Southwest.Longitude.Value, partner.Location.BoundingBox.Southwest.Longitude.Value);

                        // ReSharper restore PossibleInvalidOperationException
                    }
                }

                return _boundingBox ?? (_boundingBox = new BoundingBox
                {
                    Northeast = new Coordinates { Latitude = 90, Longitude = 180 },
                    Southwest = new Coordinates { Latitude = -90, Longitude = -180 },
                });
            }
        }

        public Guid EntityId { get; set; }

        public string UmbrellaType { get; set; }
        public Guid? UmbrellaEntityId { get; set; }

        public string Type { get; set; }
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartsOn { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ExpiresOn { get; set; }
        public bool IsExpirationEstimated { get; set; }
        public bool? IsAutoRenew { get; set; }
        public string Status { get; set; }

        public EstablishmentInfo[] Owners { get; set; }
        public EstablishmentInfo[] Partners { get; set; }

        public class EstablishmentInfo
        {
            public Guid EntityId { get; set; }
            public string OfficialName { get; set; }
            public string TranslatedName { get; set; }
            public LocationInfo Location { get; set; }
            public class LocationInfo
            {
                public double? CenterLatitude { get; set; }
                public double? CenterLongitude { get; set; }
                public BoundingBox BoundingBox { get; set; }
                public int GoogleMapZoomLevel { get; set; }

                public PlaceInfo[] Places { get; set; }
                public class PlaceInfo
                {
                    public string OfficialName { get; set; }
                    public bool IsEarth { get; set; }
                    public bool IsContinent { get; set; }
                    public bool IsCountry { get; set; }
                    public bool IsAdmin1 { get; set; }
                    public bool IsAdmin2 { get; set; }
                    public bool IsAdmin3 { get; set; }
                    public string GeoPlanetPlaceType { get; set; }
                    public bool IsCity
                    {
                        get { return "Town".Equals(GeoPlanetPlaceType, StringComparison.OrdinalIgnoreCase); }
                    }
                }

                public PlaceInfo[] DistinctPlaces
                {
                    get
                    {
                        var places = new List<PlaceInfo>();
                        foreach (var place in Places)
                        {
                            if (place.IsEarth) continue;
                            if (!place.IsContinent && !place.IsCountry && !place.IsAdmin1 && !place.IsAdmin2 && !place.IsAdmin3 && !place.IsCity)
                                continue;
                            var existingPlace = places.SingleOrDefault(p => p.OfficialName.Equals(place.OfficialName));
                            if (existingPlace == null)
                            {
                                places.Add(place);
                                continue;
                            }

                            if (place.IsCity)
                            {
                                places.Remove(existingPlace);
                                places.Add(place);
                            }
                        }
                        return places.ToArray();
                    }
                }
            }

            public EmailDomainInfo[] EmailDomains { get; set; }
            public class EmailDomainInfo
            {
                public string Value { get; set; }
            }
        }

        public ContactInfo[] Contacts { get; set; }

        public class ContactInfo
        {
            public string Type { get; set; }
            public PersonInfo Person { get; set; }
            public class PersonInfo
            {
                public string DisplayName { get; set; }
                public string DefaultEmail { get; set; }
            }
        }

        public FileInfo[] Files { get; set; }

        public class FileInfo
        {
            public Guid EntityId { get; set; }
            public string Name { get; set; }
            public string MimeType { get; set; }
        }

    }
}