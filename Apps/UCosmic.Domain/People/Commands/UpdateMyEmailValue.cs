using System;
using System.Security.Principal;
using FluentValidation;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public class UpdateMyEmailValueCommand
    {
        public IPrincipal Principal { get; set; }
        public int Number { get; set; }
        public string NewValue { get; set; }
        public bool ChangedState { get; internal set; }
    }

    public class UpdateMyEmailValueHandler : IHandleCommands<UpdateMyEmailValueCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;

        public UpdateMyEmailValueHandler(IProcessQueries queryProcessor, ICommandEntities entities)
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
        }

        public void Handle(UpdateMyEmailValueCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            command.ChangedState = false;

            // get the email address
            var email = _queryProcessor.Execute(
                new GetMyEmailAddressByNumberQuery
                {
                    Principal = command.Principal,
                    Number = command.Number,
                }
            );

            // only process matching email
            if (email == null) return;

            // only update the value if it was respelled
            if (email.Value == command.NewValue) return;

            email.Value = command.NewValue;
            _entities.Update(email);
            command.ChangedState = true;
        }
    }

    public class UpdateMyEmailValueValidator : AbstractValidator<UpdateMyEmailValueCommand>
    {
        private readonly IProcessQueries _queryProcessor;

        public UpdateMyEmailValueValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Principal)

                // principal cannot be null
                .NotEmpty().WithMessage(
                    ValidatePrincipal.FailedBecausePrincipalWasNull)

                // principal identity name cannot be null or whitespace
                .Must(ValidatePrincipal.IdentityNameIsNotEmpty).WithMessage(
                    ValidatePrincipal.FailedBecauseIdentityNameWasEmpty)

                // principal identity name must match User.Name entity property
                .Must(ValidatePrincipalIdentityNameMatchesUser).WithMessage(
                    ValidatePrincipal.FailedBecauseIdentityNameMatchedNoUser,
                        p => p.Principal.Identity.Name)
            ;

            RuleFor(p => p.Number)

                // number must match email for user
                .Must(ValidateEmailAddressNumberAndPrincipalMatchesEntity).WithMessage(
                    ValidateEmailAddress.FailedBecauseNumberAndPrincipalMatchedNoEntity,
                        p => p.Number)
            ;

            RuleFor(p => p.NewValue)

                // new email address cannot be empty
                .NotEmpty().WithMessage(
                    ValidateEmailAddress.FailedBecauseValueWasEmpty)

                // must be valid against email address regular expression
                .EmailAddress().WithMessage(
                    ValidateEmailAddress.FailedBecauseValueWasNotValidEmailAddress,
                        p => p.NewValue)

                // must match previous spelling case insensitively
                .Must(ValidateEmailAddressNewValueMatchesCurrentValueCaseInsensitively).WithMessage(
                    ValidateEmailAddress.FailedBecauseNewValueDidNotMatchCurrentValueCaseInvsensitively,
                        p => p.NewValue)
            ;
        }

        private EmailAddress _email;

        private bool ValidatePrincipalIdentityNameMatchesUser(IPrincipal principal)
        {
            return ValidatePrincipal.IdentityNameMatchesUser(principal, _queryProcessor);
        }

        private bool ValidateEmailAddressNumberAndPrincipalMatchesEntity(UpdateMyEmailValueCommand command, int number)
        {
            return ValidateEmailAddress.NumberAndPrincipalMatchesEntity(number, command.Principal, _queryProcessor, out _email);
        }

        private bool ValidateEmailAddressNewValueMatchesCurrentValueCaseInsensitively(string newValue)
        {
            return ValidateEmailAddress.NewValueMatchesCurrentValueCaseInsensitively(newValue, _email);
        }
    }
}
