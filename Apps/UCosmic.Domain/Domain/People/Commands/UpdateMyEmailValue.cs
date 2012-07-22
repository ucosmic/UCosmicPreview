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
        private readonly ICommandEntities _entities;

        public UpdateMyEmailValueHandler(ICommandEntities entities)
        {
            _entities = entities;
        }

        public void Handle(UpdateMyEmailValueCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            command.ChangedState = false;

            // get the email address
            var email = _entities.Get<EmailAddress>()
                .ByUserNameAndNumber(command.Principal.Identity.Name, command.Number);

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
        public UpdateMyEmailValueValidator(IQueryEntities entities)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            EmailAddress email = null;

            RuleFor(p => p.Principal)
                // principal cannot be null
                .NotEmpty()
                    .WithMessage(ValidatePrincipal.FailedBecausePrincipalWasNull)
                // principal identity name cannot be null or whitespace
                .Must(ValidatePrincipal.IdentityNameIsNotEmpty)
                    .WithMessage(ValidatePrincipal.FailedBecauseIdentityNameWasEmpty)
                // principal identity name must match User.Name entity property
                .Must(p => ValidatePrincipal.IdentityNameMatchesUser(p, entities))
                    .WithMessage(ValidatePrincipal.FailedBecauseIdentityNameMatchedNoUser,
                        p => p.Principal.Identity.Name)
            ;

            RuleFor(p => p.Number)
                // number must match email for user
                .Must((o, p) => ValidateEmailAddress.NumberAndPrincipalMatchesEntity(p, o.Principal, entities, out email))
                    .When(p => p.Principal != null && p.Principal.Identity.Name != null)
                    .WithMessage(ValidateEmailAddress.FailedBecauseNumberAndPrincipalMatchedNoEntity,
                        p => p.Number, p => p.Principal.Identity.Name)
            ;

            RuleFor(p => p.NewValue)
                // new email address cannot be empty
                .NotEmpty()
                    .WithMessage(ValidateEmailAddress.FailedBecauseValueWasEmpty)
                // must be valid against email address regular expression
                .EmailAddress()
                    .WithMessage(ValidateEmailAddress.FailedBecauseValueWasNotValidEmailAddress,
                        p => p.NewValue)
            ;

            RuleFor(p => p.NewValue)
                // must match previous spelling case insensitively
                .Must(p => ValidateEmailAddress.NewValueMatchesCurrentValueCaseInsensitively(p, email))
                .When(p => email != null)
                    .WithMessage(ValidateEmailAddress.FailedBecauseNewValueDidNotMatchCurrentValueCaseInvsensitively,
                        p => p.NewValue)
            ;
        }
    }
}
