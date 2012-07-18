using System;
using System.Collections.Generic;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentQuery : RevisableEntityQueryCriteria<Establishment>
    {
        public bool? HasParent { get; set; }
        public bool? HasChildren { get; set; }
        public string EmailDomain { get; set; }
        public string WebsiteUrl { get; set; }
        public string AutoCompleteTerm { get; set; }
        public string SamlEntityId { get; set; }
    }

    public static class EstablishmentBy
    {
        public static EstablishmentQuery SamlEntityId(string samlEntityId)
        {
            return new EstablishmentQuery { SamlEntityId = samlEntityId };
        }

        public static EstablishmentQuery EmailDomain(string emailDomain)
        {
            return new EstablishmentQuery { EmailDomain = emailDomain };
        }

        public static EstablishmentQuery WebsiteUrl(string websiteUrl)
        {
            return new EstablishmentQuery { WebsiteUrl = websiteUrl };
        }
    }

    public static class EstablishmentsWith
    {
        public static EstablishmentQuery NoParentButWithChildren()
        {
            return new EstablishmentQuery { HasParent = false, HasChildren = true };
        }

        public static EstablishmentQuery AutoCompleteTerm(string autoCompleteTerm, 
            int? maxResults = null)
        {
            return new EstablishmentQuery
            {
                AutoCompleteTerm = autoCompleteTerm,
                MaxResults = maxResults,
            };
        }

        public static EstablishmentQuery AutoCompleteTerm(string autoCompleteTerm,
            ICollection<int> excludeRevisionIds = null, int? maxResults = null)
        {
            return new EstablishmentQuery
            {
                AutoCompleteTerm = autoCompleteTerm,
                MaxResults = maxResults,
                ExcludeRevisionIds = excludeRevisionIds,
            };
        }

        public static EstablishmentQuery AutoCompleteTerm(string autoCompleteTerm,
            ICollection<Guid> excludeEntityIds = null, int? maxResults = null)
        {
            return new EstablishmentQuery
            {
                AutoCompleteTerm = autoCompleteTerm,
                MaxResults = maxResults,
                ExcludeEntityIds = excludeEntityIds,
            };
        }
    }
}