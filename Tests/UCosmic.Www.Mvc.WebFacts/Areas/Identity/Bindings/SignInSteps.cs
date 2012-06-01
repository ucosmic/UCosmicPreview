using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.Areas.Common;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    [Binding]
    public class SignInSteps
    {
        [Given(@"I am signed in as (.*)")]
        public void SignIn(string email)
        {
            SignInWithSpecialPassword(email, "asdfasdf");
        }

        [Given(@"I am signed in as (.*) using password ""(.*)""")]
        public void SignInWithSpecialPassword(string email, string password)
        {
            var nav = new NavigationSteps();
            var button = new ButtonSteps();
            var text = new TextFieldSteps();
            var link = new LinkSteps();

            nav.GoToPage(HomePage.TitleText);
            nav.GoToPage(SignOutPage.TitleText);
            SignInEvents.ClearSigningEmailAddress();
            nav.GoToPage(SignOnPage.TitleText);
            nav.SeePage(SignOnPage.TitleText);
            text.TypeIntoTextField(email, SignOnPage.EmailAddressLabel);
            button.ClickLabeledSubmitButton(SignOnPage.SubmitButtonLabel);
            nav.SeePage(SignInPage.TitleText);
            text.TypeIntoTextField(password, SignInPage.PasswordLabel);
            button.ClickLabeledSubmitButton(SignInPage.SubmitButtonLabel);
            nav.SeePage(MyHomePage.TitleText);
            link.SeeLinkWithText("Sign Out");
        }
    }
}
