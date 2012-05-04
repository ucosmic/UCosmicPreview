using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    // ReSharper disable UnusedMember.Global
    public class ResetPasswordProfilerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheEntityToViewModelProfile
        {
            [TestMethod]
            public void MapsToken()
            {
                var source = new EmailConfirmation();

                var destination = Mapper.Map<ResetPasswordForm>(source);

                destination.ShouldNotBeNull();
                destination.Token.ShouldEqual(source.Token);
            }

            [TestMethod]
            public void IgnoresPassword()
            {
                var source = new EmailConfirmation();

                var destination = Mapper.Map<ResetPasswordForm>(source);

                destination.ShouldNotBeNull();
                destination.Password.ShouldBeNull();
            }

            [TestMethod]
            public void IgnoresPasswordConfirmation()
            {
                var source = new EmailConfirmation();

                var destination = Mapper.Map<ResetPasswordForm>(source);

                destination.ShouldNotBeNull();
                destination.PasswordConfirmation.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheViewModelToCommandProfile
        {
            [TestMethod]
            public void MapsToken()
            {
                var source = new ResetPasswordForm
                {
                    Token = Guid.NewGuid(),
                };

                var destination = Mapper.Map<ResetPasswordCommand>(source);

                destination.ShouldNotBeNull();
                destination.Token.ShouldEqual(source.Token);
            }

            [TestMethod]
            public void MapsPassword()
            {
                var source = new ResetPasswordForm
                {
                    Password = "password",
                };

                var destination = Mapper.Map<ResetPasswordCommand>(source);

                destination.ShouldNotBeNull();
                destination.Password.ShouldEqual(source.Password);
            }

            [TestMethod]
            public void MapsPasswordConfirmation()
            {
                var source = new ResetPasswordForm
                {
                    PasswordConfirmation = "password confirmation",
                };

                var destination = Mapper.Map<ResetPasswordCommand>(source);

                destination.ShouldNotBeNull();
                destination.PasswordConfirmation.ShouldEqual(source.PasswordConfirmation);
            }

            [TestMethod]
            public void IgnoresTicket()
            {
                var source = new ResetPasswordForm();

                var destination = Mapper.Map<ResetPasswordCommand>(source);

                destination.ShouldNotBeNull();
                destination.Ticket.ShouldBeNull();
            }
        }
    }
}
