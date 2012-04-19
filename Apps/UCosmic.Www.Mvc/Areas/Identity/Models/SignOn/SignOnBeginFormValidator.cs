using FluentValidation;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignOn
{
    public class SignOnBeginFormValidator : AbstractValidator<SignOnBeginForm>
    {
        private readonly IProcessQueries _queryProcessor;

        public SignOnBeginFormValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;

            RuleFor(p => p.EmailAddress)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .SignOnEmailAddressRules()
                .Must(MatchExistingEstablishment).WithMessage(IneligibleEmailMessage, p => p.EmailAddress)
            ;
        }

        public const string IneligibleEmailMessage = "Sorry, but the email address \"{0}\" is not eligible at this time.";

        private bool MatchExistingEstablishment(string emailAddress)
        {
            var establishment = _queryProcessor.Execute(
                new GetEstablishmentByEmailQuery
                {
                    Email = emailAddress,
                }
            );

            return establishment != null && establishment.IsMember;
        }
    }
}