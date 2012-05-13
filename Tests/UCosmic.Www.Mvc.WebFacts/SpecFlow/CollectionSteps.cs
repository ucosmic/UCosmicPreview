using System;
using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace UCosmic.Www.Mvc
{
    [Binding]
    public class CollectionSteps : BaseStepDefinition
    {
        [Then(@"I should see an item for ""(.*)"" in the (.*) list")]
        [Then(@"I should still see an item for ""(.*)"" in the (.*) list")]
        public void SeeItemInList(string expectedValue, string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                browser.WaitUntil(b => page.GetCollectionItems(fieldLabel).Any(ElementTextEquals(expectedValue)),
                    "The expected item '{0}' was not found in the '{1}' list by @Browser"
                        .FormatWith(expectedValue, fieldLabel));

                var item = page.GetCollectionItems(fieldLabel).Single(ElementTextEquals(expectedValue));
                browser.WaitUntil(b => item.Displayed,
                    "The expected item '{0}' was not displayed in the '{1}' list on @Browser (actual value was '{2}')."
                        .FormatWith(expectedValue, fieldLabel, item.Text));
            });
        }

        [Then(@"I shouldn't see an item for ""(.*)"" in the (.*) list")]
        [Then(@"I should not see an item for ""(.*)"" in the (.*) list")]
        public void DoNotSeeItemInList(string unexpectedValue, string fieldLabel)
        {
            Browsers.ForEach(browser => browser.WaitUntil(b =>
                b.GetPage().GetCollectionItems(fieldLabel) == null ||
                !b.GetPage().GetCollectionItems(fieldLabel).Any(ElementTextEquals(unexpectedValue)) ||
                !b.GetPage().GetCollectionItems(fieldLabel).Any(li => li.Text.Equals(unexpectedValue) && li.Displayed),
                    "An item with text '{0}' was unexpectedly displayed for the '{1}' field using @Browser."
                        .FormatWith(unexpectedValue, fieldLabel)));
        }

        [When(@"I click the remove icon for ""(.*)"" in the (.*) list")]
        public void RemoveItemFromList(string itemText, string fieldLabel)
        {
            var key = Content.CollectionItemRemoveFormat.FormatWith(itemText);

            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var link = page.GetCollectionItem(fieldLabel, key);
                link.ClickLink();
            });
        }

        [When(@"I click the ""(.*)"" link in the (.*) list")]
        [When(@"I click the ""(.*)"" link in the (.*) list again")]
        public void ClickLinkInList(string itemText, string fieldLabel)
        {
            var key = Content.CollectionItemClickFormat.FormatWith(itemText);

            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var link = page.GetCollectionItem(fieldLabel, key);
                link.ClickLink();
            });
        }

        [Then(@"I should see a ""(.*)"" link for item \#(.*) in the (.*) list")]
        [Then(@"I should see an ""(.*)"" link for item \#(.*) in the (.*) list")]
        [Then(@"I should still see a ""(.*)"" link for item \#(.*) in the (.*) list")]
        [Then(@"I should still see an ""(.*)"" link for item \#(.*) in the (.*) list")]
        public void SeeLinkInNumberedListItem(string linkText, int itemNumber, string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                browser.WaitUntil(b => page.GetCollectionItems(fieldLabel, Content.CollectionItemToken).IsNotNull(),
                    "@Browser found no items for the '{0}' list."
                        .FormatWith(fieldLabel));
                var items = page.GetCollectionItems(fieldLabel, Content.CollectionItemToken);
                browser.WaitUntil(b => items.IsNotNull() && items.Count() >= itemNumber,
                    "@Browser did not find at least {1} item{2} in the '{0}' list."
                        .FormatWith(fieldLabel, itemNumber, itemNumber == 1 ? null : "s"));
                var item = items.Skip(itemNumber - 1).Take(1).SingleOrDefault();
                browser.WaitUntil(b => item != null,
                    "@Browser did not find item #{1} in the '{0}' list."
                        .FormatWith(fieldLabel, itemNumber));
                if (item == null) throw new NotSupportedException();
                var link = browser.WaitUntil(b => item.FindElement(By.LinkText(linkText)),
                    "@Browser did not find a '{2}' link for item #{1} in the '{0}' list."
                        .FormatWith(fieldLabel, itemNumber, linkText));
                browser.WaitUntil(b => link.Displayed,
                    "@Browser did not display a '{2}' link for item #{1} in the '{0}' list."
                        .FormatWith(fieldLabel, itemNumber, linkText));
            });
        }

        [When(@"I click the ""(.*)"" link for item \#(.*) in the (.*) list")]
        [When(@"I click the ""(.*)"" link for item \#(.*) in the (.*) list again")]
        public void ClickLinkInNumberedListItem(string linkText, int itemNumber, string fieldLabel)
        {
            SeeLinkInNumberedListItem(linkText, itemNumber, fieldLabel);

            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var items = page.GetCollectionItems(fieldLabel, Content.CollectionItemToken);
                var item = items.Skip(itemNumber - 1).Take(1).Single();
                var link = item.FindElement(By.LinkText(linkText));
                link.ClickLink();
            });
        }

        [Then(@"I should see a (.*) text field for item \#(.*) in the (.*) list")]
        [Then(@"I should see an (.*) text field for item \#(.*) in the (.*) list")]
        public void SeeTextFieldInNumberedListItem(string textLabel, int itemNumber, string collectionLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                browser.WaitUntil(b => page.GetCollectionItems(collectionLabel, Content.CollectionItemToken).IsNotNull(),
                    "@Browser found no items for the '{0}' list."
                        .FormatWith(collectionLabel));
                var items = page.GetCollectionItems(collectionLabel, Content.CollectionItemToken);
                browser.WaitUntil(b => items.IsNotNull() && items.Count() >= itemNumber,
                    "@Browser did not find at least {1} item{2} in the '{0}' list."
                        .FormatWith(collectionLabel, itemNumber, itemNumber == 1 ? null : "s"));
                var item = items.Skip(itemNumber - 1).Take(1).SingleOrDefault();
                browser.WaitUntil(b => item != null,
                    "@Browser did not find item #{1} in the '{0}' list."
                        .FormatWith(collectionLabel, itemNumber));
                if (item == null) throw new NotSupportedException();
                var textField = browser.WaitUntil(b => item.FindElement(By.CssSelector(page.Fields[textLabel])),
                    "@Browser could not find a '{0}' text field for item #{1} in the '{2}' list."
                        .FormatWith(textLabel, itemNumber, collectionLabel));
                browser.WaitUntil(b => textField.Displayed,
                    "@Browser did not display a '{0}' text foe;d for item #{1} in the '{2}' list."
                        .FormatWith(textLabel, itemNumber, collectionLabel));
            });
        }

        [When(@"I type ""(.*)"" into the (.*) text field for item \#(.*) in the (.*) list")]
        public void TypeIntoTextFieldInNumberedListItem(string textToType, string textLabel, int itemNumber, string collectionLabel)
        {
            SeeTextFieldInNumberedListItem(textLabel, itemNumber, collectionLabel);

            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var items = page.GetCollectionItems(collectionLabel, Content.CollectionItemToken);
                var item = items.Skip(itemNumber - 1).Take(1).SingleOrDefault();
                if (item == null) throw new NotSupportedException();
                var textField = item.FindElement(By.CssSelector(page.Fields[textLabel]));
                textField.Clear();
                textField.SendKeys(textToType);
            });
        }
    }
}
