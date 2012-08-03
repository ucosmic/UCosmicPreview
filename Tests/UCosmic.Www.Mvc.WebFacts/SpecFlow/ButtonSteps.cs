using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace UCosmic.Www.Mvc
{
    [Binding]
    public class ButtonSteps : BaseStepDefinition
    {
        #region Labeled Buttons

        public void FindLabeledButton(string label)
        {
            var cssSelector = LabeledButtonCssFormat.FormatWith(label);

            Browsers.ForEach(browser => browser.WaitUntil(b =>
                b.TryFindElement(By.CssSelector(cssSelector)).IsNotNull(),
                "Button labeled '{0}' was not found by @Browser (using CSS selector {1})"
                    .FormatWith(label, cssSelector)));
        }

        [Then(@"I should see a ""(.*)"" button")]
        public void SeeLabeledButton(string label)
        {
            FindLabeledButton(label);

            var cssSelector = LabeledButtonCssFormat.FormatWith(label);

            Browsers.ForEach(browser => browser.WaitUntil(b =>
                b.FindElement(By.CssSelector(cssSelector)).Displayed,
                "Button labeled '{0}' was not displayed by @Browser (using CSS selector {1})"
                    .FormatWith(label, cssSelector)));
        }

        [When(@"I click the ""(.*)"" button")]
        public void ClickLabeledButton(string label)
        {
            SeeLabeledButton(label);

            var cssSelector = LabeledButtonCssFormat.FormatWith(label);

            Browsers.ForEach(browser =>
            {
                var button = browser.FindElement(By.CssSelector(cssSelector));
                button.ClickButton();
            });
        }

        private const string LabeledButtonCssFormat = "form input[type=button][value='{0}']";

        #endregion
        #region Labeled Submit Buttons

        public void FindLabeledSubmitButton(string label)
        {
            var cssSelector = LabeledSubmitButtonCssFormat.FormatWith(label);

            Browsers.ForEach(browser => browser.WaitUntil(b =>
                b.TryFindElement(By.CssSelector(cssSelector)).IsNotNull(),
                "Submit button labeled '{0}' was not found by @Browser (using CSS selector {1})"
                    .FormatWith(label, cssSelector)));
        }
        [Then(@"I should see a ""(.*)"" submit button")]
        public void SeeLabeledSubmitButton(string label)
        {
            FindLabeledSubmitButton(label);

            var cssSelector = LabeledSubmitButtonCssFormat.FormatWith(label);

            Browsers.ForEach(browser => browser.WaitUntil(b =>
                b.GetElement(By.CssSelector(cssSelector)).Displayed,
                "Submit button labeled '{0}' was not displayed by @Browser (using CSS selector {1})"
                    .FormatWith(label, cssSelector)));
        }

        [When(@"I click the ""(.*)"" submit button")]
        public void ClickLabeledSubmitButton(string label)
        {
            SeeLabeledSubmitButton(label);

            var cssSelector = LabeledSubmitButtonCssFormat.FormatWith(label);

            Browsers.ForEach(browser =>
            {
                var button = browser.WaitUntil(b => b.GetElement(By.CssSelector(cssSelector)), null);
                button.ClickButton();
            });
        }

        [Then(@"the ""(.*)"" submit button should be disabled")]
        [Then(@"the ""(.*)"" submit button should not be enabled")]
        [Then(@"the ""(.*)"" submit button shouldn't be enabled")]
        public void EnsureLabeledSubmitButtonIsDisabled(string label)
        {
            FindLabeledSubmitButton(label);

            var cssSelector = LabeledSubmitButtonCssFormat.FormatWith(label);

            Browsers.ForEach(browser =>
            {
                var button = browser.FindElement(By.CssSelector(cssSelector));
                browser.WaitUntil(b => "true".Equals(button.GetAttribute("disabled")),
                    "Submit button labeled '{0}' was not disabled in @Browser."
                        .FormatWith(label));
            });
        }

        [Then(@"the ""(.*)"" submit button should not be disabled")]
        [Then(@"the ""(.*)"" submit button shouldn't be disabled")]
        [Then(@"the ""(.*)"" submit button should be enabled")]
        [Then(@"the ""(.*)"" submit button should become enabled")]
        public void EnsureLabeledSubmitButtonIsNotDisabled(string label)
        {
            FindLabeledSubmitButton(label);

            var cssSelector = LabeledSubmitButtonCssFormat.FormatWith(label);

            Browsers.ForEach(browser =>
            {
                var button = browser.FindElement(By.CssSelector(cssSelector));

                browser.WaitUntil(b => "false".Equals(button.GetAttribute("disabled"))
                        || button.GetAttribute("disabled") == null
                        || browser.ExecuteScript(string.Format(@"return $(""{0}"").attr('disabled');", cssSelector)) == null, // hack for IE server
                    "Submit button labeled '{0}' was unexpectedly disabled in @Browser."
                        .FormatWith(label));
            });
        }

        private const string LabeledSubmitButtonCssFormat = "form input[type=submit][value='{0}']";

        #endregion
        #region Unlabeled Submit Buttons

        public void FindUnlabeledSubmitButton()
        {
            Browsers.ForEach(browser => browser.WaitUntil(b =>
                b.TryFindElement(By.CssSelector(UnlabeledSubmitButtonCss)).IsNotNull(),
                "Submit button was not found by @Browser (using CSS selector {0})."
                    .FormatWith(UnlabeledSubmitButtonCss)));
        }

        [Then(@"I should see a submit button")]
        public void SeeUnlabeledSubmitButton()
        {
            FindUnlabeledSubmitButton();

            Browsers.ForEach(browser => browser.WaitUntil(b =>
                b.TryFindElement(By.CssSelector(UnlabeledSubmitButtonCss)).IsNotNull() &&
                b.FindElement(By.CssSelector(UnlabeledSubmitButtonCss)).Displayed,
                "Submit button was not displayed by @Browser (using CSS selector {0})."
                    .FormatWith(UnlabeledSubmitButtonCss)));
        }

        [When(@"I click the submit button")]
        public void ClickUnlabeledSubmitButton()
        {
            SeeUnlabeledSubmitButton();

            Browsers.ForEach(browser =>
            {
                var button = browser.FindElement(By.CssSelector(UnlabeledSubmitButtonCss));
                button.ClickButton();
            });
        }

        private const string UnlabeledSubmitButtonCss = "form input[type=submit]";

        #endregion
    }
}
