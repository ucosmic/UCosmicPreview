using System;

namespace UCosmic.Domain.People
{
    public class EmailConfirmation : Entity
    {
        protected EmailConfirmation()
        {
            Token = Guid.NewGuid();
            IssuedOnUtc = DateTime.UtcNow;
            ExpiresOnUtc = DateTime.UtcNow.AddHours(2);
        }

        protected internal EmailConfirmation(EmailConfirmationIntent intent)
            : this()
        {
            Intent = intent;
        }

        public int Id { get; protected internal set; }

        public int PersonId { get; protected internal set; }
        public int EmailAddressNumber { get; protected internal set; }
        public virtual EmailAddress EmailAddress { get; protected internal set; }

        public Guid Token { get; protected set; }

        public string SecretCode { get; protected internal set; }

        public string Ticket { get; protected internal set; }

        public string IntentText { get; protected set; }
        public EmailConfirmationIntent Intent
        {
            get { return IntentText.AsEnum<EmailConfirmationIntent>(); }
            protected set { IntentText = value.AsSentenceFragment(); }
        }

        public DateTime IssuedOnUtc { get; protected internal set; }

        public DateTime ExpiresOnUtc { get; protected internal set; }
        public bool IsExpired { get { return (DateTime.UtcNow > ExpiresOnUtc); } }

        public DateTime? RedeemedOnUtc { get; protected internal set; }
        public bool IsRedeemed { get { return RedeemedOnUtc.HasValue; } }

        public DateTime? RetiredOnUtc { get; protected internal set; }
        public bool IsRetired { get { return RetiredOnUtc.HasValue; } }
    }
}