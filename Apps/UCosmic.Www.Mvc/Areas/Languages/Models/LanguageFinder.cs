using System.Collections.Generic;

namespace UCosmic.Www.Mvc.Areas.Languages.Models
{
    public class LanguageFinder
    {
        public string Keyword { get; set; }
        public IEnumerable<LanguageResult> Results { get; set; }
    }
}