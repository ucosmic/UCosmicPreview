using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.Password
{
    public class ChangePasswordForm : IReturnUrl
    {
        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Current password", Prompt = "Type the password you want to change")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [UIHint("StrengthMeteredPassword")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(100, ErrorMessage = "Your password must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password", Prompt = "Type your new password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Password confirmation is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "New password confirmation", Prompt = "Type your new password again to confirm")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}