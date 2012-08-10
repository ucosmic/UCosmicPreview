using System.ComponentModel.DataAnnotations;

namespace UCosmic.Www.Mvc.Areas.Languages.Models
{
    public class LanguagesRequest
    {
        public LanguagesRequest()
        {
            Keyword = "";
            Size = 10;
            Number = 1;
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Keyword { get; set; }

        public int Size { get; set; }
        public int Number { get; set; }
    }
}