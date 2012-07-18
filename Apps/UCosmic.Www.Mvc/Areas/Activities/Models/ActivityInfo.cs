using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using AutoMapper;
using ServiceLocatorPattern;
using UCosmic.Domain.Activities;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;
using UCosmic.Domain.Places;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Activities.Models
{
    public class ActivityInfo
    {
        public string Title { get; set; }

        [DataType(DataType.Html)]
        public IHtmlString Content { get; set; }

        public DateTime StartsOn { get; set; }
        public DateTime? EndsOn { get; set; }
        public int Number { get; set; }
        public Guid EntityId { get; set; }
        public OwnedBy Person { get; set; }
        public class OwnedBy
        {
            public string DisplayName { get; set; }
            public string UserName { get; set; }
            public string DefaultAffiliationJobTitles { get; set; }
            public int DefaultAffiliationEstablishmentId { get; set; }
            public string DefaultAffiliationEstablishmentOfficialName { get; set; }
        }

        public Tag[] Tags { get; set; }
        public class Tag
        {
            public string Text { get; set; }
        }

        public PlaceMark[] PlaceMarks { get; set; }
        public class PlaceMark
        {
            public double CenterLatitude { get; set; }
            public double CenterLongitude { get; set; }
            public BoundingBox BoundingBox { get; set; }
            public string Title { get; set; }
        }

        private BoundingBox _boundingBox;
        public BoundingBox MapBoundingBox
        {
            get
            {
                if (_boundingBox == null)
                {
                    foreach (var placeMark in PlaceMarks.Where(t => t.BoundingBox.HasValue))
                    {
                        if (_boundingBox == null)
                        {
                            _boundingBox = placeMark.BoundingBox;
                            continue;
                        }
                        // ReSharper disable PossibleInvalidOperationException

                        // northern latitudes are positive, southern latitudes are negative
                        _boundingBox.Northeast.Latitude = Math.Max(_boundingBox.Northeast.Latitude.Value, placeMark.BoundingBox.Northeast.Latitude.Value);
                        _boundingBox.Southwest.Latitude = Math.Min(_boundingBox.Southwest.Latitude.Value, placeMark.BoundingBox.Southwest.Latitude.Value);

                        _boundingBox.Northeast.Longitude = Math.Max(_boundingBox.Northeast.Longitude.Value, placeMark.BoundingBox.Northeast.Longitude.Value);
                        _boundingBox.Southwest.Longitude = Math.Min(_boundingBox.Southwest.Longitude.Value, placeMark.BoundingBox.Southwest.Longitude.Value);

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
    }

    public static class ActivityInfoProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(ActivityInfoProfiler));
        }

        private class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Activity, ActivityInfo>()
                    .ForMember(d => d.Title, o => o.MapFrom(s => s.Values.Title))
                    .ForMember(d => d.Content, o => o.MapFrom(s => new HtmlString(s.Values.Content)))
                    .ForMember(d => d.StartsOn, o => o.MapFrom(s => s.Values.StartsOn))
                    .ForMember(d => d.EndsOn, o => o.MapFrom(s => s.Values.EndsOn))
                    .ForMember(d => d.PlaceMarks, o => o.ResolveUsing(s =>
                    {
                        var queryProcessor = ServiceProviderLocator.Current.GetService<IProcessQueries>();
                        var placeMarks = new List<ActivityInfo.PlaceMark>();
                        foreach (var tag in s.Tags.Where(t => t.DomainType != ActivityTagDomainType.Custom))
                        {
                            if (tag.DomainType == ActivityTagDomainType.Place && tag.DomainKey.HasValue)
                            {
                                var place = queryProcessor.Execute(
                                    new GetPlaceByIdQuery
                                    {
                                        Id = tag.DomainKey.Value,
                                    }
                                );
                                if (place != null && place.Center != null && place.Center.HasValue)
                                {
                                    var placeMark = new ActivityInfo.PlaceMark
                                    {
                                        // ReSharper disable PossibleInvalidOperationException
                                        CenterLatitude = place.Center.Latitude.Value,
                                        CenterLongitude = place.Center.Longitude.Value,
                                        // ReSharper restore PossibleInvalidOperationException
                                        BoundingBox = place.BoundingBox,
                                        Title = place.OfficialName,
                                    };
                                    placeMarks.Add(placeMark);
                                }
                            }
                            else if (tag.DomainType == ActivityTagDomainType.Establishment && tag.DomainKey.HasValue)
                            {
                                var establishment = queryProcessor.Execute(
                                    new GetEstablishmentByIdQuery(tag.DomainKey.Value)
                                    {
                                        EagerLoad = new Expression<Func<Establishment, object>>[]
                                        {
                                            e => e.Location,
                                        }
                                    }
                                );
                                if (establishment != null && establishment.Location.Center != null && establishment.Location.Center.HasValue)
                                {
                                    var placeMark = new ActivityInfo.PlaceMark
                                    {
                                        // ReSharper disable PossibleInvalidOperationException
                                        CenterLatitude = establishment.Location.Center.Latitude.Value,
                                        CenterLongitude = establishment.Location.Center.Longitude.Value,
                                        // ReSharper restore PossibleInvalidOperationException
                                        BoundingBox = establishment.Location.BoundingBox,
                                        Title = establishment.TranslatedName.Text,
                                    };
                                    placeMarks.Add(placeMark);
                                }
                            }
                        }
                        return placeMarks;
                    }))
                ;

                CreateMap<ActivityTag, ActivityInfo.Tag>();
                CreateMap<Person, ActivityInfo.OwnedBy>();
            }
        }
    }
}