using TechTalk.SpecFlow;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    [Binding]
    public class SignOutSteps : BaseStepDefinition
    {
        [Given(@"I am not signed on")]
        [When(@"I am not signed on")]
        public void SignOut()
        {
            new NavigationSteps().GoToPage(SignOutPage.TitleText);
        }
    }
}
