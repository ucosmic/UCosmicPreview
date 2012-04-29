using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using FluentValidation;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public class UpdateEmailValueForm : IReturnUrl
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = ValueDisplayName)]
        [Remote("ValidateValue", "UpdateEmailValue", "My", HttpMethod = "POST", AdditionalFields = "PersonUserName,Number")]
        public string Value { get; set; }
        public const string ValuePropertyName = "Value";
        public const string ValueDisplayName = "New spelling";

        [Display(Name = OldSpellingDisplayName)]
        public string OldSpelling { get; set; }
        public const string OldSpellingDisplayName = "Current spelling";

        [HiddenInput(DisplayValue = false)]
        public string PersonUserName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Number { get; set; }
    }

    public class UpdateEmailValueValidator : AbstractValidator<UpdateEmailValueForm>
    {
        public const string FailedBecausePreviousSpellingDoesNotMatchValueCaseInsensitively
            = "You can only change lowercase letters to uppercase (or vice versa) when changing the spelling of your email address.";

        public UpdateEmailValueValidator(IProcessQueries queryProcessor)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            EmailAddress email = null;

            RuleFor(p => p.Value)
                // email address cannot be empty
                .NotEmpty()
                    .WithMessage(FailedBecausePreviousSpellingDoesNotMatchValueCaseInsensitively)
                // must be valid against email address regular expression
                .EmailAddress()
                    .WithMessage(FailedBecausePreviousSpellingDoesNotMatchValueCaseInsensitively)
                // validate the number within the Value property b/c remote only validates this property
                .Must((o, p) => ValidateEmailAddress.NumberAndPrincipalMatchesEntity(o.Number, o.PersonUserName.AsPrincipal(), queryProcessor, out email))
                    .WithMessage(ValidateEmailAddress.FailedBecauseNumberAndPrincipalMatchedNoEntity,
                        p => p.Number, p => p.PersonUserName)
                // must match previous spelling case insensitively
                .Must(p => ValidateEmailAddress.NewValueMatchesCurrentValueCaseInsensitively(p, email))
                    .WithMessage(FailedBecausePreviousSpellingDoesNotMatchValueCaseInsensitively)
            ;
        }
    }

    public static class UpdateEmailValueProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(UpdateEmailValueProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToViewModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<EmailAddress, UpdateEmailValueForm>()
                    .ForMember(d => d.OldSpelling, opt => opt.MapFrom(s => s.Value))
                    .ForMember(d => d.ReturnUrl, opt => opt.Ignore())
                ;
            }
        }

        private class ViewModelToCommandProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<UpdateEmailValueForm, UpdateMyEmailValueCommand>()
                    .ForMember(d => d.Principal, o => o.Ignore())
                    .ForMember(d => d.NewValue, o => o.MapFrom(s => s.Value))
                    .ForMember(d => d.ChangedState, o => o.Ignore())
                ;
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}