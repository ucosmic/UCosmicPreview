using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.Common.WebPages;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    public class InstitutionalAgreementForm : WebPageBase
    {
        public InstitutionalAgreementForm(IWebDriver driver) : base(driver) { }

        protected override Dictionary<string, string> SpecToWeb
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "Participant Search", "participant_search" },
                    { "Participant Search[AutoComplete]", ".ParticipantSearch-field" },
                    { "Agreement Type", "Type" },
                    { "Agreement Type[ErrorText='Required']", "Agreement type is required." },
                    { "Agreement Type[DownArrow]", ".Type-field button" },
                    { "Agreement Type[AutoComplete]", ".Type-field" },
                    { "Summary Description", "Title" },
                    { "Summary Description[ErrorText='Required']", "Summary description is required." },
                    { "Start Date", "StartsOn" },
                    { "Start Date[ErrorText='Required']", "Start date is required." },
                    { "Expiration Date", "ExpiresOn" },
                    { "Expiration Date[ErrorText='Required']", "Expiration date is required." },
                    { "Current Status", "Status" },
                    { "Current Status[ErrorText='Required']", "Current status is required." },
                    { "Current Status[DownArrow]", ".Status-field button" },
                    { "Current Status[AutoComplete]", ".Status-field" },
                };
            }
        }

    }
}
