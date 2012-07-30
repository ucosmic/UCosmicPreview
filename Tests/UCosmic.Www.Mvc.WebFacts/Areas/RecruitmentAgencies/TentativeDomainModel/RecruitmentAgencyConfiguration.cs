using System.Collections.Generic;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;

namespace UCosmic.Www.Mvc.Areas.RecruitmentAgencies.TentativeDomainModel
{
    public class RecruitmentAgencyConfiguration : RevisableEntity
    {
        // primary key is the primary key of the establishment
        public int ForEstablishmentRevisionId { get; set; }
        public Establishment ForEstablishment { get; set; }

        public bool IsWelcomeMessageEnabled { get; set; }
        public string WelcomeMessageTitle { get; set; }
        public string WelcomeMessageContent { get; set; }
        public string WelcomeMessageDraft { get; set; }

        public ICollection<NotificationEmailAddress> NotificationEmails { get; set; }
    }
}
