using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.Passwords.Models
{
    // ReSharper disable UnusedMember.Global
    public class ResetPasswordQueryProfilerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheQueryToFormProfile
        {
            [TestMethod]
            public void MapsToken()
            {
                var value = Guid.NewGuid();
                var source = new ResetPasswordQuery { Token = value };

                var destination = Mapper.Map<ResetPasswordForm>(source);

                destination.ShouldNotBeNull();
                destination.Token.ShouldEqual(source.Token);
            }

            [TestMethod]
            public void IgnoresPassword()
            {
                var source = new ResetPasswordQuery();

                var destination = Mapper.Map<ResetPasswordForm>(source);

                destination.ShouldNotBeNull();
                destination.Password.ShouldBeNull();
            }

            [TestMethod]
            public void IgnoresPasswordConfirmation()
            {
                var source = new ResetPasswordQuery();

                var destination = Mapper.Map<ResetPasswordForm>(source);

                destination.ShouldNotBeNull();
                destination.PasswordConfirmation.ShouldBeNull();
            }
        }
    }
}
