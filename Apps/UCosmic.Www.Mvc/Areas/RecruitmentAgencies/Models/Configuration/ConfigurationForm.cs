using System.ComponentModel.DataAnnotations;

namespace UCosmic.Www.Mvc.Areas.RecruitmentAgencies.Models.Configuration
{

    public class ConfigurationForm
    {
        [Display(Name = "Welcome Message", Prompt = "Configure custom content to be displayed before a recruitment agency applies to represent you")]
        public string WelcomeMessage { get; set; }

        [Display(Name = "Notifications", Prompt = "Designate one or more email addresses to be notified when a new recruitment agency application is received")]
        public string Notifications { get; set; }
    }
}
