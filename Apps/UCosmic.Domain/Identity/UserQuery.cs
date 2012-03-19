using System;
using System.Collections.Generic;

namespace UCosmic.Domain.Identity
{
    public class UserQuery : RevisableEntityQueryCriteria<User>
    {
        public string UserName { get; set; }
        public string Saml2SubjectNameId { get; set; }
        public string RoleGrant { get; set; }
        public string AutoCompleteTerm { get; set; }
    }

    public static class UserBy
    {
        public static UserQuery UserName(string userName)
        {
            return new UserQuery { UserName = userName };
        }

        public static UserQuery Saml2SubjectNameId(string saml2SubjectNameId)
        {
            return new UserQuery { Saml2SubjectNameId = saml2SubjectNameId };
        }
    }

    public static class UsersWith
    {
        public static UserQuery RoleGrant(string roleName)
        {
            return new UserQuery { RoleGrant = roleName };
        }

        public static UserQuery AutoCompleteTerm(string autoCompleteTerm,
            ICollection<int> excludeRevisionIds = null, int? maxResults = null)
        {
            return new UserQuery
            {
                AutoCompleteTerm = autoCompleteTerm,
                MaxResults = maxResults,
                ExcludeRevisionIds = excludeRevisionIds,
            };
        }

        public static UserQuery AutoCompleteTerm(string autoCompleteTerm,
            ICollection<Guid> excludeEntityIds = null, int? maxResults = null)
        {
            return new UserQuery
            {
                AutoCompleteTerm = autoCompleteTerm,
                MaxResults = maxResults,
                ExcludeEntityIds = excludeEntityIds,
            };
        }
    }
}