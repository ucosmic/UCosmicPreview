using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignOn
{
    [Validator(typeof(SignOnBeginFormValidator))]
    public class SignOnBeginForm : IReturnUrl
    {
        public const string EmailAddressRequiredMessage = "Email Address is required.";
        public const string EmailAddressRegexMessage = "Please enter a valid email address.";
        public const string EmailAddressDisplayName = "Email Address";
        public const string EmailAddressWatermark = "Enter your work email address";

        [DataType(DataType.EmailAddress)]
        [UIHint("SignOnEmailAddress")]
        [Display(Name = EmailAddressDisplayName, Prompt = EmailAddressWatermark)]
        public string EmailAddress { get; set; }

        //[DataType(DataType.Password)]
        //[UIHint("SignOnPassword")]
        //[Display(Name = "Password", Prompt = "Enter your password")]
        //public string Password { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}
