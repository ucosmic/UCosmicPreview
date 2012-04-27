using System;
using System.Collections.Generic;
using System.Linq;
using UCosmic.Domain.Email;

namespace UCosmic.Domain.People
{
    public class EmailAddress : Entity, IAmNumbered
    {
        public EmailAddress()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Confirmations = new List<EmailConfirmation>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public int PersonId { get; protected internal set; }
        public virtual Person Person { get; protected internal set; }
        public int Number { get; protected internal set; }

        public string Value { get; set; }

        public bool IsDefault { get; set; }

        public bool IsFromSaml { get; protected internal set; }

        public bool IsConfirmed { get; set; }

        public virtual ICollection<EmailConfirmation> Confirmations { get; protected internal set; }

        public EmailConfirmation AddConfirmation(string intent)
        {
            var confirmation = new EmailConfirmation
            {
                Intent = intent,
                SecretCode = RandomSecretCreator.CreateSecret(12),
                EmailAddress = this,
            };
            Confirmations.Add(confirmation);
            return confirmation;
        }

        public override string ToString()
        {
            return Value ?? base.ToString();
        }

        public bool Confirm(Guid token, string intent, string secretCode)
        {
            if (token != Guid.Empty)
            {
                var confirmation = Confirmations.SingleOrDefault(c =>
                    c.Token == token && c.Intent == intent && c.ExpiresOnUtc > DateTime.UtcNow
                        && c.SecretCode == secretCode);
                if (confirmation != null)
                {
                    IsConfirmed = true;
                    confirmation.RedeemedOnUtc = DateTime.UtcNow;
                    confirmation.SecretCode = null;
                    return true;
                }
            }
            return false;
        }
    }
}