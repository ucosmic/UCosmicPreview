using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Languages.Models
{
    public class LanguagesLayout
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

        public LanguagesLayout()
        {
            LensOptions = _lensOptions.Select(layoutOption => new SelectListItem
            {
                Text = layoutOption.ToString(),
                Value = layoutOption.ToString(),
            })
            .ToArray();

            PageSizeOptions = _pageSizeOptions.Select(pageSizeOption => new SelectListItem
            {
                Value = pageSizeOption.Key.ToInvariantString(), Text = pageSizeOption.Value,
            })
            .ToArray();
        }

        public SelectListItem[] LensOptions { get; private set; }

        public ItemsLens Lens { get; set; }

        public SelectListItem[] PageSizeOptions { get; private set; }

        public int SelectedPageSize { get; set; }
    }
}