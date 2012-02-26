using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.Password
{
    [TestClass]
    public class ChangePasswordFormTests
    {
        [TestMethod]
        public void ViewModel_Identity_Password_ChangePasswordForm_ShouldBeConstructible()
        {
            var model = new ChangePasswordForm
            {
                OldPassword = "old password",
                NewPassword = "new password",
                ConfirmPassword = "confirm password",
            };

            model.ShouldNotBeNull();
        }
    }
}
