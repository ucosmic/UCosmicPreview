using System;
using System.Collections.Generic;

namespace UCosmic.Domain.Identity
{
    public class UserQuery : RevisableEntityQueryCriteria<User>
    {
        public string RoleGrant { get; set; }
        public string AutoCompleteTerm { get; set; }
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