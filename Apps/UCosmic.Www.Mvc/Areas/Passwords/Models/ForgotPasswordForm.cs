using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Passwords.Models
{
    public class ForgotPasswordForm : IReturnUrl
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = EmailAddressDisplayName, Prompt = EmailAddressDisplayPrompt)]
        [Remote("ValidateEmailAddress", "ForgotPassword", "Passwords", HttpMethod = "POST")]
        public string EmailAddress { get; set; }
        public const string EmailAddressPropertyName = "EmailAddress";
        public const string EmailAddressDisplayName = "Email Address";
        public const string EmailAddressDisplayPrompt = "Enter the email address you used when you signed up";

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}