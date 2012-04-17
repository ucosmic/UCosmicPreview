using System;
using System.Collections.Generic;
using System.Linq;
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

        public int Id { get; set; }

        public int EmailAddressId { get; set; }
        public virtual EmailAddress EmailAddress { get; set; }
        public int EmailAddressNumber { get; protected internal set; }

        public Guid Token { get; set; }

        public string SecretCode { get; set; }

        public string Intent { get; set; }

        public DateTime IssuedOnUtc { get; set; }
        public DateTime? ConfirmedOnUtc { get; set; }
        public DateTime ExpiresOnUtc { get; set; }

        public bool IsExpired { get { return (DateTime.UtcNow > ExpiresOnUtc); } }

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

        //public IDictionary<string, string> GetMessageVariables(IManageConfigurations config)
        //{
        //    var tokenAsString = Token.ToString();
        //    var variables = new Dictionary<string, string>
        //    {
        //        { "{EmailAddress}", EmailAddress.Value },
        //        { "{ConfirmationCode}", SecretCode },
        //    };
        //    switch (Intent)
        //    {
        //        case EmailConfirmationIntent.SignUp:
        //            variables.Add("{ConfirmationUrl}", string.Format(config.SignUpEmailConfirmationUrlFormat,
        //                tokenAsString, SecretCode.UrlEncoded()));
        //            variables.Add("{StartUrl}", config.SignUpUrl);
        //            break;

        //        case EmailConfirmationIntent.PasswordReset:
        //            variables.Add("{ConfirmationUrl}", string.Format(config.PasswordResetConfirmationUrlFormat,
        //                tokenAsString, SecretCode.UrlEncoded()));
        //            variables.Add("{PasswordResetUrl}", config.PasswordResetUrl);
        //            break;
        //    }
        //    return variables;
        //}

    }

    public static class EmailConfirmationIntent
    {
        public const string SignUp = "Sign Up";
        public const string PasswordReset = "Password Reset";
    }

    public static class EmailConfirmationExtensions
    {
        public static EmailConfirmation ByToken(this IEnumerable<EmailConfirmation> enumerable, Guid token)
        {
            return (enumerable != null && token != Guid.Empty)
                ? enumerable.SingleOrDefault(c => c.Token == token)
                : null;
        }

    }
}