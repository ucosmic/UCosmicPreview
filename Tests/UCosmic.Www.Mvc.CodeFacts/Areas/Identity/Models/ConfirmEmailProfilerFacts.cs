using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    // ReSharper disable UnusedMember.Global
    public class ConfirmEmailProfilerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheEntityToViewModelProfile
        {
            [TestMethod]
            public void MapsToken()
            {
                var source = new EmailConfirmation();

                var destination = Mapper.Map<ConfirmEmailForm>(source);

                destination.ShouldNotBeNull();
                destination.Token.ShouldEqual(source.Token);
            }

            [TestMethod]
            public void MapsIntent()
            {
                var source = new EmailConfirmation
                {
                    Intent = "intent",
                };

                var destination = Mapper.Map<ConfirmEmailForm>(source);

                destination.ShouldNotBeNull();
                destination.Intent.ShouldEqual(source.Intent);
            }

            [TestMethod]
            public void IgnoresSecretCode()
            {
                var source = new EmailConfirmation
                {
                    SecretCode = "its a secret",
                };

                var destination = Mapper.Map<ConfirmEmailForm>(source);

                // SECRET CODE MUST NOT MAP FROM ENTITY TO MODEL!
                destination.ShouldNotBeNull();
                destination.SecretCode.ShouldBeNull();
            }

            [TestMethod]
            public void IgnoresIsUrlConfirmation()
            {
                var source = new EmailConfirmation();

                var destination = Mapper.Map<ConfirmEmailForm>(source);

                destination.ShouldNotBeNull();
                destination.IsUrlConfirmation.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheViewModelToCommandProfile
        {
            [TestMethod]
            public void MapsToken()
            {
                var source = new ConfirmEmailForm
                {
                    Token = Guid.NewGuid(),
                };

                var destination = Mapper.Map<RedeemEmailConfirmationCommand>(source);

                destination.Token.ShouldEqual(source.Token);
            }

            [TestMethod]
            public void MapsSecretCode()
            {
                var source = new ConfirmEmailForm
                {
                    SecretCode = "its a secret",
                };

                var destination = Mapper.Map<RedeemEmailConfirmationCommand>(source);

                destination.SecretCode.ShouldEqual(source.SecretCode);
            }

            [TestMethod]
            public void MapsIntent()
            {
                var source = new ConfirmEmailForm
                {
                    Intent = "intent",
                };

                var destination = Mapper.Map<RedeemEmailConfirmationCommand>(source);

                destination.Intent.ShouldEqual(source.Intent);
            }

            [TestMethod]
            public void IgnoresTicket()
            {
                var source = new ConfirmEmailForm();

                var destination = Mapper.Map<RedeemEmailConfirmationCommand>(source);

                destination.Ticket.ShouldBeNull();
            }
        }
    }
}
