using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.WebDriver;
using UCosmic.Www.Mvc.Areas.Common.WebPages;

namespace UCosmic.Www.Mvc.SpecFlow
{
    [Binding]
    public class CheckBoxSteps : BaseStepDefinition
    {
        [Given(@"I (.*)ed the (.*) checkbox")]
        [When(@"I (.*) the (.*) checkbox")]
        [Then(@"I should (.*) the (.*) checkbox")]
        public void CheckCheckBox(string checkOrUncheck, string fieldLabel)
        {
            var shouldCheck = (checkOrUncheck == "check");
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var checkBox = page.GetCheckBox(fieldLabel);
                checkBox.CheckOrUncheckCheckBox(shouldCheck);
            });
        }
    }
}
