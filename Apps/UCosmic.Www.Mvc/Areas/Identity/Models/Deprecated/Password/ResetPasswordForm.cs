using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.Password
{
    public class ResetPasswordForm
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Token { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string EmailAddressValue { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string SecretCode { get; set; }

        [UIHint("StrengthMeteredPassword")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Your password must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Enter your new password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation", Prompt = "Enter the same password again to confirm")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}