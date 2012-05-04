//using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;

//namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignUp
//{
//    public class OldCreatePasswordForm
//    {
//        public const string PasswordRequiredErrorMessage = "Password is required.";
//        public const string ConfirmationRequiredErrorMessage = "Password confirmation is required.";
//        public const string PasswordLengthErrorMessage = "Your password must be at least {2} characters long (but no more than {1}).";
//        public const string PasswordCompareErrorMessage = "The password and confirmation password do not match.";

//        [UIHint("StrengthMeteredPassword")]
//        [Required(ErrorMessage = PasswordRequiredErrorMessage)]
//        [StringLength(100, MinimumLength = 6, ErrorMessage = PasswordLengthErrorMessage)]
//        [DataType(DataType.Password)]
//        [Display(Name = "Password")]
//        public string Password { get; set; }

//        [Required(ErrorMessage = ConfirmationRequiredErrorMessage)]
//        [DataType(DataType.Password)]
//        [Display(Name = "Confirmation")]
//        [Compare("Password", ErrorMessage = PasswordCompareErrorMessage)]
//        public string ConfirmPassword { get; set; }
//    }
//}