using System;

namespace UCosmic.Domain.Email
{
    public class SendPasswordResetMessageCommand
    {
        public string EmailAddress { get; set; }
        public Guid ConfirmationToken { get; internal set; }
    }
}
