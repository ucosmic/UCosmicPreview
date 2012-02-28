using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Attributes;
using UCosmic.Www.Mvc.Models;
using UCosmic.Domain;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignOn
{
    [Validator(typeof(SignOnFormValidator))]
    public class SignOnForm : IReturnUrl
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address", Prompt = "Enter your work email address")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Enter your password")]
        public string Password { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

        public bool ShowPasswordInput { get; set; }
    }

    public class SignOnFormValidator : AbstractValidator<SignOnForm>
    {
        public SignOnFormValidator(IQueryEntities queryEntities)
        {
            RuleFor(p => p.EmailAddress).NotEmpty().WithMessage("Email Address is required.");
        }
    }
}
