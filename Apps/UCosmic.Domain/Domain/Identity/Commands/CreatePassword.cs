using System;
using System.Linq.Expressions;
using FluentValidation;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Identity
{
    public class CreatePasswordCommand
    {
        public Guid Token { get; set; }
        public string Ticket { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }

    public class CreatePasswordHandler : IHandleCommands<CreatePasswordCommand>
    {
        private readonly ICommandEntities _entities;
        private readonly IStorePasswords _passwords;

        public CreatePasswordHandler(ICommandEntities entities
            , IStorePasswords passwords
        )
        {
            _entities = entities;
            _passwords = passwords;
        }

        public void Handle(CreatePasswordCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // get the confirmation
            var confirmation = _entities.Get2<EmailConfirmation>()
                .EagerLoad(new Expression<Func<EmailConfirmation, object>>[]
                {
                    c => c.EmailAddress.Person.User.EduPersonScopedAffiliations,
                    c => c.EmailAddress.Person.User.SubjectNameIdentifiers,
                }, _entities)
                .ByToken(command.Token);

            // set up user accounts
            var person = confirmation.EmailAddress.Person;
            person.User = person.User ?? new User();
            person.User.Name = person.User.Name ?? confirmation.EmailAddress.Value;
            person.User.IsRegistered = true;
            person.User.EduPersonTargetedId = null;
            person.User.EduPersonScopedAffiliations.Clear();
            person.User.SubjectNameIdentifiers.Clear();

            confirmation.RetiredOnUtc = DateTime.UtcNow;
            confirmation.SecretCode = null;
            confirmation.Ticket = null;
            _entities.Update(confirmation);

            _passwords.Create(confirmation.EmailAddress.Person.User.Name, command.Password);
        }
    }

    public class CreatePasswordValidator : AbstractValidator<CreatePasswordCommand>
    {
        public CreatePasswordValidator(IQueryEntities entities, IStorePasswords passwords)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            EmailConfirmation confirmation = null;

            RuleFor(p => p.Token)
                // token cannot be an empty guid
                .NotEmpty()
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseTokenWasEmpty,
                        p => p.Token)
                // token must match a confirmation
                .Must(p => ValidateEmailConfirmation.TokenMatchesEntity(p, entities, out confirmation))
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseTokenMatchedNoEntity,
                        p => p.Token)
            ;

            RuleFor(p => p.Ticket)
                // ticket cannot be empty
                .NotEmpty()
                    .WithMessage(ValidateEmailConfirmation.FailedBecauseTicketWasEmpty)
            ;

            RuleFor(p => p.Password)
                // cannot be empty
                .NotEmpty()
                    .WithMessage(ValidatePassword.FailedBecausePasswordWasEmpty)
                // length must be between 6 and 100 characters
                .Length(passwords.MinimumPasswordLength, int.MaxValue)
                    .WithMessage(ValidatePassword.FailedBecausePasswordWasTooShort(passwords.MinimumPasswordLength))
            ;

            RuleFor(p => p.PasswordConfirmation)
                // cannot be empty
                .NotEmpty()
                    .WithMessage(ValidatePassword.FailedBecausePasswordConfirmationWasEmpty)
            ;

            RuleFor(p => p.PasswordConfirmation)
                // must match password unless password is invalid or password confirmation is empty
                .Equal(p => p.Password)
                    .Unless(p =>
                        string.IsNullOrWhiteSpace(p.PasswordConfirmation) ||
                        string.IsNullOrWhiteSpace(p.Password) ||
                        p.Password.Length < passwords.MinimumPasswordLength)
                    .WithMessage(ValidatePassword.FailedBecausePasswordConfirmationDidNotEqualPassword)
            ;

            // when confirmation is not null,
            When(p => confirmation != null, () =>
            {
                RuleFor(p => p.Token)
                    // its intent must be to sign up
                    .Must(p => confirmation.Intent == EmailConfirmationIntent.CreatePassword)
                        .WithMessage(ValidateEmailConfirmation.FailedBecauseIntentWasIncorrect,
                            p => confirmation.Intent, p => confirmation.Token)
                    // it cannot be expired
                    .Must(p => !confirmation.IsExpired)
                        .WithMessage(ValidateEmailConfirmation.FailedBecauseIsExpired,
                            p => confirmation.Token, p => confirmation.ExpiresOnUtc)
                    // it cannot be retired
                    .Must(p => !confirmation.IsRetired)
                        .WithMessage(ValidateEmailConfirmation.FailedBecauseIsRetired,
                            p => confirmation.Token, p => confirmation.RetiredOnUtc)
                    // it must be redeemed
                    .Must(p => confirmation.IsRedeemed)
                        .WithMessage(ValidateEmailConfirmation.FailedBecauseIsNotRedeemed,
                            p => confirmation.Token)
                    // email address must be confirmed
                    .Must(p => ValidateEmailAddress.IsConfirmed(confirmation.EmailAddress))
                        .WithMessage(ValidateEmailAddress.FailedBecauseIsNotConfirmed,
                            p => confirmation.EmailAddress.Value)
                    // user, if present, cannot match local member account
                    .Must(p => confirmation.EmailAddress.Person.User == null
                        || !passwords.Exists(confirmation.EmailAddress.Person.User.Name))
                        .WithMessage(ValidateUser.FailedBecauseNameMatchedLocalMember,
                            p => confirmation.EmailAddress.Person.User.Name)
                ;

                RuleFor(p => p.Ticket)
                    // its ticket must match the command ticket
                    .Must(p => ValidateEmailConfirmation.TicketIsCorrect(confirmation, p))
                        .WithMessage(ValidateEmailConfirmation.FailedBecauseTicketWasIncorrect,
                            p => p.Ticket, p => p.Token)
                ;
            });
        }
    }
}
