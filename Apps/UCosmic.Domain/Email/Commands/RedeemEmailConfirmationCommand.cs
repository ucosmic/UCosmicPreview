using System;

namespace UCosmic.Domain.Email
{
    public class RedeemEmailConfirmationCommand
    {
        public Guid Token { get; set; }
        public string SecretCode { get; set; }
        public string Intent { get; set; }
        public string Ticket { get; internal set; }
    }
}
