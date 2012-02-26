using System.ComponentModel.DataAnnotations;
using UCosmic.Www.Mvc.Models;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.Password
{
    public class ForgotPasswordForm : IReturnUrl
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address", Prompt = "Enter the email address you used when you signed up")]
        [Required(ErrorMessage = "Email Address is required.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "This is not a valid email address.")]
        [ForgotPasswordEmail(ErrorMessage = "A user account with the email address '{0}' could not be found.")]
        public string EmailAddress { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}