using TechTalk.SpecFlow;

namespace UCosmic.Www.Mvc
{
    [Binding]
    public class RadioButtonSteps : BaseStepDefinition
    {
        [Then(@"I should see a (.*) radio button labeled '(.*)'")]
        [Then(@"I should see an (.*) radio button labeled '(.*)'")]
        public void SeeRadioButton(string fieldLabel, string radioLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var radioButton = page.GetField(fieldLabel.RadioKey(radioLabel));
                browser.WaitUntil(b => radioButton.Displayed,
                    "The '{0}' radio button labeled '{1}' was not displayed by @Browser."
                        .FormatWith(fieldLabel, radioLabel));
            });
        }

        [Then(@"the (.*) radio button labeled '(.*)' options should be unchecked")]
        [Then(@"the (.*) radio button labeled '(.*)' options should not be checked")]
        [Then(@"the (.*) radio button labeled '(.*)' options shouldn't be checked")]
        public void EnsureRadioButtonIsUnchecked(string fieldLabel, string radioLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                browser.WaitUntil(b => !page.IsChecked(fieldLabel.RadioKey(radioLabel)),
                    "The '{0}' radio button labeled '{1}' was unexpectedly checked in @Browser."
                        .FormatWith(fieldLabel, radioLabel));
            });
        }

        [Then(@"the (.*) radio button labeled '(.*)' should be checked")]
        public void EnsureRadioButtonIsChecked(string fieldLabel, string radioLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                browser.WaitUntil(b => page.IsChecked(fieldLabel.RadioKey(radioLabel)),
                    "The '{0}' radio button labeled '{1}' was unchecked in @Browser."
                        .FormatWith(fieldLabel, radioLabel));
            });
        }

        [When(@"I check the (.*) radio button labeled '(.*)'")]
        public void CheckRadioButton(string fieldLabel, string radioLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var radioButton = page.GetField(fieldLabel.RadioKey(radioLabel));
                radioButton.ClickRadioButton();
            });
            EnsureRadioButtonIsChecked(fieldLabel, radioLabel);
        }
    }
}
