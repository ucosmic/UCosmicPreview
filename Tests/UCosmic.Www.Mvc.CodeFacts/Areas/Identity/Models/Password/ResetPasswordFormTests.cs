using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.Password
{
    [TestClass]
    public class ResetPasswordFormTests
    {
        [TestMethod]
        public void ViewModel_Identity_Password_ResetPasswordForm_ShouldBeConstructible()
        {
            var model = new ResetPasswordForm
            {
                Token = Guid.NewGuid(),
                EmailAddressValue = "email address value",
                SecretCode = "its a secret",
                Password = "password",
                ConfirmPassword = "confirm password",
            };

            model.ShouldNotBeNull();
        }
    }
}
