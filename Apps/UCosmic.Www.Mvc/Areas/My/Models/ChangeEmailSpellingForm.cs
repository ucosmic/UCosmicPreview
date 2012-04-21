using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public class ChangeEmailSpellingForm : IReturnUrl
    {
        public const string ValuePropertyName = "Value";
        public const string ValueDisplayName = "New spelling";
        [Display(Name = ValueDisplayName)]
        [Remote("ValidateValue", "ChangeEmailSpelling", "My", HttpMethod = "POST", AdditionalFields = "PersonUserName,Number")]
        public string Value { get; set; }

        public const string OldSpellingDisplayName = "Current spelling";
        [Display(Name = OldSpellingDisplayName)]
        public string OldSpelling { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string PersonUserName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Number { get; set; }
    }
}