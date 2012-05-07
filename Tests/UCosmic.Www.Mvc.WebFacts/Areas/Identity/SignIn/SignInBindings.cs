using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.SpecFlow;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    [Binding]
    public class SignInBindings
    {
        [AfterScenario("ClearSigningEmailAddress")]
        public void ClearSigningEmailAddress()
        {
            new NavigationSteps().GoToPage(NamedUrl.SignDown);
        }

        [Given(@"I am signed in as (.*)")]
        public void SignIn(string email)
        {
            SignInWithSpecialPassword(email, "asdfasdf");
        }

        [Given(@"I am signed in as (.*) using password ""(.*)""")]
        public void SignInWithSpecialPassword(string email, string password)
        {
            var nav = new NavigationSteps();
            var form = new FormSteps();
            var link = new LinkSteps();

            nav.GoToPage(NamedUrl.Home);
            nav.GoToPage(NamedUrl.SignOut);
            ClearSigningEmailAddress();
            nav.GoToPage(NamedUrl.SignOn);
            form.TypeIntoTextBox(email, SignOnForm.EmailAddressLabel);
            form.ClickLabeledSubmitButton(SignOnForm.SubmitButtonLabel);
            nav.SeePage(NamedUrl.EnterPassword);
            form.TypeIntoTextBox(password, SignInForm.PasswordLabel);
            form.ClickLabeledSubmitButton(SignInForm.SubmitButtonLabel);
            nav.SeePage(NamedUrl.PersonalHome);
            link.SeeLinkWithText("Sign Out");
        }

    }
}
