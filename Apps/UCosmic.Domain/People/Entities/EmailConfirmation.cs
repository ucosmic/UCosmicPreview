using System;

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

        public int Id { get; protected internal set; }

        public int PersonId { get; protected internal set; }
        public int EmailAddressNumber { get; protected internal set; }
        public virtual EmailAddress EmailAddress { get; protected internal set; }

        public Guid Token { get; protected set; }

        public string SecretCode { get; set; }

        public string Ticket { get; protected internal set; }

        public string Intent { get; set; }

        public DateTime IssuedOnUtc { get; protected internal set; }

        public DateTime ExpiresOnUtc { get; protected internal set; }

        public DateTime? RedeemedOnUtc { get; set; }

        public DateTime? RetiredOnUtc { get; set; }

        public bool IsExpired { get { return (DateTime.UtcNow > ExpiresOnUtc); } }

        public bool IsRedeemed { get { return RedeemedOnUtc.HasValue; } }

        public bool IsRetired { get { return RetiredOnUtc.HasValue; } }
    }

    // TODO: replace with enum? rename to ResetPassword and CreatePassword?
    public static class EmailConfirmationIntent
    {
        public const string SignUp = "Sign Up";
        public const string PasswordReset = "Password Reset";
    }
}