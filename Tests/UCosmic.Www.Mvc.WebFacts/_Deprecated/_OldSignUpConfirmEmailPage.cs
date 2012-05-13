//using System.Collections.Generic;
//using OpenQA.Selenium;
//using UCosmic.Www.Mvc.Areas.Common.WebPages;

//namespace UCosmic.Www.Mvc.Areas.Identity.WebPages
//{
//    public class SignUpConfirmEmailPage : WebPageBase
//    {
//        public SignUpConfirmEmailPage(IWebDriver driver)
//            : base(driver) { }

//        protected override string EditorSelector { get { return "#confirm_email_editor"; } }

//        protected override string EmailExcerptStart { get { return "enter the following Confirmation Code: \r\n"; } }
//        protected override string EmailExcerptEnd { get { return "\r\n"; } }

//        protected override Dictionary<string, string> SpecToWeb
//        {
//            get
//            {
//                return new Dictionary<string, string>
//                {
//                    { "Confirmation Code", "SecretCode" },
//                    { "Confirm Email Address", "submit" },
//                };
//            }
//        }

//    }
//}
