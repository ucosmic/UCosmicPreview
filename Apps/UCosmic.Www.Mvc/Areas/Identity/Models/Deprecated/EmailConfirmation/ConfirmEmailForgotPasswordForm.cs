using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.EmailConfirmation
{
    public class ConfirmEmailForgotPasswordForm
    {
        [HiddenInput(DisplayValue = false)]
        public string EmailAddressValue { get; set; }

        [Required(ErrorMessage = "Please enter a confirmation code.")]
        [Display(Name = "Confirmation Code")]
        [DataType(DataType.Password)]
        public string SecretCode { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required]
        public Guid Token { get; set; }
    }
}