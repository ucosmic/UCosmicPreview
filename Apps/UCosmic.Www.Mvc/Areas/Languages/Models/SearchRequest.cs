using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.Preferences.Models;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Languages.Models
{
    public class SearchRequest
    {
        private readonly ItemsLens[] _lensOptions = new[]
        {
            ItemsLens.Table,
            ItemsLens.List,
            ItemsLens.Grid,
        };

        private readonly IEnumerable<KeyValuePair<int, string>> _pageSizeOptions = new Dictionary<int, string>
        {
            { 10, "Show 10 Results" },
            { 20, "Show 20 Results" },
            { 50, "Show 50 Results" },
            { 100, "Show 100 Results" },
            { int.MaxValue, "Show all Results" },
        };

        public SearchRequest()
        {
            Keyword = "";
            PageSize = 10;
            PageNumber = 1;

            LensOptions = _lensOptions.Select(o => new SelectListItem
            {
                Value = o.ToString(), Text = o.ToString(),
            })
            .ToArray();

            PageSizeOptions = _pageSizeOptions.Select(o => new SelectListItem
            {
                Value = o.Key.ToInvariantString(), Text = o.Value,
            })
            .ToArray();
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Keyword { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
        public SelectListItem[] PageSizeOptions { get; private set; }

        public ItemsLens Lens { get; set; }
        public SelectListItem[] LensOptions { get; private set; }


    }

    public static class SearchRequestProfiler
    {
        public class PreferencesToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<IEnumerable<Preference>, SearchRequest>()
                    .ForMember(d => d.Lens, o => o.ResolveUsing(s =>
                        {
                            var lens = s.ByKey(PreferenceKey.Lens).SingleOrDefault();
                            return lens != null ? lens.Value.AsEnum<ItemsLens>() : ItemsLens.Table;
                        }))
                    .ForMember(d => d.PageSize, o => o.ResolveUsing(s =>
                        {
                            var pageSize = s.ByKey(PreferenceKey.PageSize).SingleOrDefault();
                            return pageSize != null ? int.Parse(pageSize.Value) : 10;
                        }))
                    .ForMember(d => d.Keyword, o => o.Ignore())
                    .ForMember(d => d.PageNumber, o => o.Ignore())
                    .ForMember(d => d.PageSizeOptions, o => o.Ignore())
                    .ForMember(d => d.LensOptions, o => o.Ignore())
                ;
            }
        }
    }
}