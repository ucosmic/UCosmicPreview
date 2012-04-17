using System;
using System.Collections.Generic;
using System.Linq;
using UCosmic.Domain.Email;

namespace UCosmic.Domain.People
{
    public class EmailAddress : Entity
    {
        public EmailAddress()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Confirmations = new List<EmailConfirmation>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public int Number { get; set; }

        public string Value { get; set; }

        public bool IsDefault { get; set; }

        public bool IsFromSaml { get; protected internal set; }

        public bool IsConfirmed { get; set; }

        public virtual ICollection<EmailConfirmation> Confirmations { get; set; }

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
                    confirmation.ConfirmedOnUtc = DateTime.UtcNow;
                    confirmation.SecretCode = null;
                    return true;
                }
            }
            return false;
        }
    }

    //public class EmailAddressComparer : IComparer<EmailAddress>
    //{
    //    public int Compare(EmailAddress x, EmailAddress y)
    //    {
    //        if (x.RevisionId == y.RevisionId)
    //            return 0;

    //        // the default email should appear at the top
    //        if (y.IsDefault)
    //            return 1;
    //        if (x.IsDefault)
    //            return -1;

    //        if (y.IsConfirmed)
    //            return 1;
    //        if (x.IsConfirmed)
    //            return -1;

    //        return 0;
    //    }
    //}

    public static class EmailAddressExtensions
    {

    }
}