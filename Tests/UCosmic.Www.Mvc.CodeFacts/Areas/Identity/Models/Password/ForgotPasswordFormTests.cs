using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.Password
{
    [TestClass]
    public class ForgotPasswordFormTests
    {
        [TestMethod]
        public void ViewModel_Identity_Password_ForgotPasswordForm_ShouldBeConstructible()
        {
            var model = new ForgotPasswordForm
            {
                EmailAddress = "email address",
            };

            model.ShouldNotBeNull();
        }
    }
}
