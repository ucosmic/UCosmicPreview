//using System.Collections.Generic;
//using OpenQA.Selenium;
//using UCosmic.Www.Mvc.Areas.Common.WebPages;
//using UCosmic.Www.Mvc.Areas.Identity.Models;

//namespace UCosmic.Www.Mvc.Areas.Identity
//{
//    public class SignInForm : WebPageBase
//    {
//        public SignInForm(IWebDriver driver) : base(driver) { }

//        public const string PasswordLabel = "Password";
//        public const string SubmitButtonLabel = "Sign On";

//        protected override Dictionary<string, string> SpecToWeb
//        {
//            get
//            {
//                return new Dictionary<string, string>
//                {
//                    { PasswordLabel, "Password" },
//                    { PasswordLabel + "[ErrorText='Required']",
//                        SignInValidator.FailedBecausePasswordWasEmpty },
//                    { PasswordLabel + "[ErrorText=''Invalid with 4 remaining attempts'']",
//                        string.Format(SignInValidator.FailedBecausePasswordWasIncorrect, 4, 's') },
//                };
//            }
//        }

//    }
//}
