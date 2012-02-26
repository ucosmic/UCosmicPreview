using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Www.Mvc.Areas.Identity.Models.SignIn;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.Authentication
{
    [TestClass]
    public class SignInFormTests
    {
        [TestMethod]
        public void ViewModel_Identity_Authentication_SignInForm_ShouldBeConstructible()
        {
            var model = new SignInForm
            {
                Password = "password",
            };

            model.ShouldNotBeNull();
            model.Password.ShouldNotBeNull();
        }
    }
}
