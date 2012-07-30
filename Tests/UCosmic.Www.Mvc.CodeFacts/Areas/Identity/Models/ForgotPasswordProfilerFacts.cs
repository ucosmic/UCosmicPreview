using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public static class ForgotPasswordProfilerFacts
    {
        [TestClass]
        public class TheModelToCommandProfile
        {
            [TestMethod]
            public void IgnoresConfirmationToken()
            {
                var model = new ForgotPasswordForm();

                var command = Mapper.Map<SendConfirmEmailMessageCommand>(model);

                command.ShouldNotBeNull();
                command.ConfirmationToken.ShouldEqual(Guid.Empty);
            }

            [TestMethod]
            public void MapsEmailAddress()
            {
                const string value = "user@domain.tld";
                var model = new ForgotPasswordForm { EmailAddress = value };

                var command = Mapper.Map<SendConfirmEmailMessageCommand>(model);

                command.ShouldNotBeNull();
                command.EmailAddress.ShouldNotBeNull();
                command.EmailAddress.ShouldEqual(model.EmailAddress);
            }

            [TestMethod]
            public void MapsIntent_UsingValue_EmailConfirmationIntent_ResetPassword()
            {
                var model = new ForgotPasswordForm();

                var command = Mapper.Map<SendConfirmEmailMessageCommand>(model);

                command.ShouldNotBeNull();
                command.Intent.ShouldEqual(EmailConfirmationIntent.ResetPassword);
            }
        }
    }
}
