using System.ComponentModel.DataAnnotations;

namespace UCosmic.Www.Mvc.Areas.Languages.Models
{
    public class LanguagesRequest
    {
        public LanguagesRequest()
        {
            Keyword = "";
            PageSize = 10;
            PageNumber = 1;
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Keyword { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}