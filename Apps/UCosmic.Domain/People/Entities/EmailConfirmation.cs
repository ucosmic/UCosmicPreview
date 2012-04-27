using System;
using System.Collections.Generic;
using UCosmic.Domain.Email;

namespace UCosmic.Domain.People
{
    public class EmailConfirmation : Entity
    {
        public EmailConfirmation()
        {
            Token = Guid.NewGuid();
            IssuedOnUtc = DateTime.UtcNow;
            ExpiresOnUtc = DateTime.UtcNow.AddHours(2);
        }

        protected internal EmailConfirmation(EmailAddress emailAddress, string intent, int secretCodeLength = 12)
            : this()
        {
            Intent = intent;
            SecretCode = RandomSecretCreator.CreateSecret(secretCodeLength);
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            EmailAddress = emailAddress;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public int Id { get; protected internal set; }

        public int PersonId { get; protected internal set; }
        public int EmailAddressNumber { get; protected internal set; }
        public virtual EmailAddress EmailAddress { get; protected internal set; }

        public Guid Token { get; protected set; }

        public string SecretCode { get; set; }

        public string Ticket { get; protected internal set; }

        public string Intent { get; set; }

        public DateTime IssuedOnUtc { get; protected internal set; }

        public DateTime? RedeemedOnUtc { get; set; }

        public DateTime ExpiresOnUtc { get; protected internal set; }

        public bool IsExpired { get { return (DateTime.UtcNow > ExpiresOnUtc); } }

        public bool IsRedeemed { get { return RedeemedOnUtc.HasValue; } }

        public EmailMessage ComposeConfirmationMessage(EmailTemplate template,
            string startUrl, string confirmationUrl, IManageConfigurations config)
        {
            //var tokenAsString = Token.ToString();
            var variables = new Dictionary<string, string>
            {
                { "{EmailAddress}", EmailAddress.Value },
                { "{ConfirmationCode}", SecretCode },
                { "{StartUrl}", startUrl },
                { "{ConfirmationUrl}", confirmationUrl },
            };

            var message = template.ComposeMessageTo(EmailAddress, variables, config);
            EmailAddress.Person.Messages.Add(message);
            return message;
        }

        public IDictionary<string, string> GetMessageVariables(IManageConfigurations config)
        {
            var tokenAsString = Token.ToString();
            var variables = new Dictionary<string, string>
            {
                { "{EmailAddress}", EmailAddress.Value },
                { "{ConfirmationCode}", SecretCode },
            };
            switch (Intent)
            {
                case EmailConfirmationIntent.SignUp:
                    variables.Add("{ConfirmationUrl}", string.Format(config.SignUpEmailConfirmationUrlFormat,
                        tokenAsString, SecretCode.UrlEncoded()));
                    variables.Add("{StartUrl}", config.SignUpUrl);
                    break;

                case EmailConfirmationIntent.PasswordReset:
                    variables.Add("{ConfirmationUrl}", string.Format(config.PasswordResetConfirmationUrlFormat,
                        tokenAsString, SecretCode.UrlEncoded()));
                    variables.Add("{PasswordResetUrl}", config.PasswordResetUrl);
                    break;
            }
            return variables;
        }

    }

    public static class EmailConfirmationIntent
    {
        public const string SignUp = "Sign Up";
        public const string PasswordReset = "Password Reset";
    }
}