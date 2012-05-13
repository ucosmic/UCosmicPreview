using TechTalk.SpecFlow;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    [Binding]
    public class SignInEvents
    {
        [AfterScenario("ClearSigningEmailAddress")]
        public static void ClearSigningEmailAddress()
        {
            new NavigationSteps().GoToPage(SignDownPage.TitleText);
        }

        [AfterFeature]
        public static void SignOutAll()
        {
            new SignOutSteps().SignOut();
        }
    }
}
