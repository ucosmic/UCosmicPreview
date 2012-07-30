using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UCosmic.Domain.Activities;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Places;

namespace UCosmic.Www.Mvc.Areas.Activities.Models
{
    public class TagMenuItem
    {
        public int DomainKey { get; set; }
        public ActivityTagDomainType DomainType { get; set; }
        public string Text { get; set; }
        public string MatchingText { get; set; }
        public string TaggedType { get; set; }
        public IEnumerable<string> TaggedHierarchy { get; set; }
        public IEnumerable<string> PlaceHierarchy { get; set; }
    }

    public static class TagMenuItemProfiler
    {
        public class PlaceToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Place, TagMenuItem>()
                    .ForMember(d => d.DomainKey, o => o.MapFrom(s => s.RevisionId))
                    .ForMember(d => d.DomainType, o => o.UseValue(ActivityTagDomainType.Place))
                    .ForMember(d => d.Text, o => o.MapFrom(s => s.OfficialName))
                    .ForMember(d => d.MatchingText, o => o.Ignore())
                    .ForMember(d => d.TaggedType, o => o.MapFrom(s => s.GeoPlanetPlace != null ? s.GeoPlanetPlace.Type.EnglishName : "Place"))
                    .ForMember(d => d.TaggedHierarchy, o => o.ResolveUsing(s =>
                    {
                        var hierarchy = new List<string>();
                        if (s.Ancestors != null && s.Ancestors.Any())
                            hierarchy.AddRange(s.Ancestors
                                .Where(a => !a.Ancestor.IsEarth)
                                .OrderByDescending(a => a.Separation)
                                .Select(a => a.Ancestor.OfficialName));
                        return hierarchy;
                    }))
                    .ForMember(d => d.PlaceHierarchy, o => o.UseValue(null))
                ;
            }
        }

        public class EstablishmentToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Establishment, TagMenuItem>()
                    .ForMember(d => d.DomainKey, o => o.MapFrom(s => s.RevisionId))
                    .ForMember(d => d.DomainType, o => o.UseValue(ActivityTagDomainType.Establishment))
                    .ForMember(d => d.Text, o => o.MapFrom(s => s.TranslatedName.Text))
                    .ForMember(d => d.MatchingText, o => o.Ignore())
                    .ForMember(d => d.TaggedType, o => o.MapFrom(s => s.Type.EnglishName))
                    .ForMember(d => d.TaggedHierarchy, o => o.ResolveUsing(s =>
                    {
                        var hierarchy = new List<string>();
                        if (s.Ancestors != null && s.Ancestors.Any())
                            hierarchy.AddRange(s.Ancestors
                                .OrderByDescending(a => a.Separation)
                                .Select(a => a.Ancestor.OfficialName));
                        return hierarchy;
                    }))
                    .ForMember(d => d.PlaceHierarchy, o => o.ResolveUsing(s =>
                    {
                        var hierarchy = new List<string>();
                        if (s.Location != null && s.Location.Places != null && s.Location.Places.Any())
                            hierarchy.AddRange(s.Location.Places
                                .Where(p => !p.IsEarth)
                                .Select(a => a.OfficialName)
                                .Take(3));
                        return hierarchy;
                    }))
                ;
            }
        }
    }
}