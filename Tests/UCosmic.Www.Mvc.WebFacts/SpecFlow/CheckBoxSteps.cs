using TechTalk.SpecFlow;

namespace UCosmic.Www.Mvc
{
    [Binding]
    public class CheckBoxSteps : BaseStepDefinition
    {
        [Then(@"I should see a (.*) check box")]
        [Then(@"I should see an (.*) check box")]
        public void SeeCheckBox(string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var checkBox = page.GetField(fieldLabel);
                browser.WaitUntil(b => checkBox.Displayed,
                    "The '{0}' check box was not displayed by @Browser."
                        .FormatWith(fieldLabel));
            });
        }

        [Then(@"the (.*) check box should be unchecked")]
        [Then(@"the (.*) check box should not be checked")]
        [Then(@"the (.*) check box shouldn't be checked")]
        public void EnsureCheckBoxIsUnchecked(string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                browser.WaitUntil(b => !page.IsChecked(fieldLabel),
                    "The '{0}' check box was unexpectedly checked in @Browser."
                        .FormatWith(fieldLabel));
            });
        }

        [Then(@"the (.*) check box should be checked")]
        public void EnsureCheckBoxIsChecked(string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                browser.WaitUntil(b => page.IsChecked(fieldLabel),
                    "The '{0}' check box was unchecked in @Browser."
                        .FormatWith(fieldLabel));
            });
        }

        [When(@"I check the (.*) check box")]
        public void CheckCheckBox(string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var checkBox = page.GetField(fieldLabel);
                checkBox.CheckCheckBox();
            });
        }

        [When(@"I uncheck the (.*) check box")]
        public void UnheckCheckBox(string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var checkBox = page.GetField(fieldLabel);
                checkBox.UncheckCheckBox();
            });
        }
    }
}
