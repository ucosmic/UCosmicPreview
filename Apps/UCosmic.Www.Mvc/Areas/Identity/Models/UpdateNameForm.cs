using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentValidation;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Controllers;
using UCosmic.Www.Mvc.Areas.People.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class UpdateNameForm
    {
        private const string NoneNullDisplayText = PersonNameController.SalutationAndSuffixNullValueLabel;
        private const string UnknownNullDisplayText = "[Unknown]";

        [Display(Name = DisplayNameDisplayName, Prompt = DisplayNameDisplayPrompt)]
        public string DisplayName { get; set; }
        public const string DisplayNameDisplayName = "Display name";
        public const string DisplayNameDisplayPrompt = "Please enter a Display name.";

        [Display(Name = IsDisplayNameDerivedDisplayName)]
        public bool IsDisplayNameDerived { get; set; }
        public const string IsDisplayNameDerivedDisplayName = "Automatically generate my display name based on the fields below.";

        [Display(Name = SalutationDisplayName)]
        [DisplayFormat(NullDisplayText = SalutationNullDisplayText)]
        public string Salutation { get; set; }
        public const string SalutationDisplayName = "Salutation";
        public const string SalutationNullDisplayText = NoneNullDisplayText;

        [Display(Name = FirstNameDisplayName)]
        [DisplayFormat(NullDisplayText = FirstNameNullDisplayText)]
        public string FirstName { get; set; }
        public const string FirstNameDisplayName = "First name";
        public const string FirstNameNullDisplayText = UnknownNullDisplayText;

        [Display(Name = MiddleNameDisplayName)]
        [DisplayFormat(NullDisplayText = MiddleNameNullDisplayText)]
        public string MiddleName { get; set; }
        public const string MiddleNameDisplayName = "Middle name or initial";
        public const string MiddleNameNullDisplayText = NoneNullDisplayText;

        [Display(Name = LastNameDisplayName)]
        [DisplayFormat(NullDisplayText = LastNameNullDisplayText)]
        public string LastName { get; set; }
        public const string LastNameDisplayName = "Last name";
        public const string LastNameNullDisplayText = UnknownNullDisplayText;

        [Display(Name = SuffixDisplayName)]
        [DisplayFormat(NullDisplayText = SuffixNullDisplayText)]
        public string Suffix { get; set; }
        public const string SuffixDisplayName = "Suffix";
        public const string SuffixNullDisplayText = NoneNullDisplayText;

        public static string ReturnUrl
        {
            get { return string.Format("~/{0}", MyHomeRouter.GetRoute.UrlConstant); }
        }
    }

    public class UpdateNameValidator : AbstractValidator<UpdateNameForm>
    {
        public const string FailedBecauseDisplayNameWasEmpty = "Display name is required.";

        public UpdateNameValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.DisplayName)
                // person must have a display name
                .NotEmpty()
                    .WithMessage(FailedBecauseDisplayNameWasEmpty)
            ;
        }
    }

    public static class UpdateNameProfiler
    {
        public class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Person, UpdateNameForm>();
            }
        }

        public class ModelToCommandProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<UpdateNameForm, UpdateMyNameCommand>()
                    .ForMember(d => d.Principal, o => o.Ignore())
                    .ForMember(d => d.ChangeCount, o => o.Ignore())
                ;
            }
        }
    }
}