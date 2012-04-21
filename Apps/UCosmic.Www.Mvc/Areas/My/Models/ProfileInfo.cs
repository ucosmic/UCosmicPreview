using System.ComponentModel.DataAnnotations;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public class ProfileInfo
    {
        public string UserEduPersonTargetedId { get; set; }
        public bool CanChangePassword
        {
            get { return string.IsNullOrWhiteSpace(UserEduPersonTargetedId); }
        }

        public EmailInfo[] Emails { get; set; }
        public class EmailInfo
        {
            public int Number { get; set; }
            public string Value { get; set; }
            public bool IsDefault { get; set; }
            public bool IsConfirmed { get; set; }
        }

        public AffiliationInfo[] Affiliations { get; set; }
        public class AffiliationInfo
        {
            public int EstablishmentId { get; set; }

            public const string JobTitlesNullDisplayText = "[Job Title(s) Unknown]";
            [DisplayFormat(NullDisplayText = JobTitlesNullDisplayText)]
            public string JobTitles { get; set; }

            public bool IsAcknowledged { get; set; }
            public bool IsClaimingStudent { get; set; }
            public bool IsClaimingEmployee { get; set; }

            public EstablishmentInfo Establishment { get; set; }
            public class EstablishmentInfo
            {
                public string OfficialName { get; set; }
                public bool IsInstitution { get; set; }
            }
        }
    }
}