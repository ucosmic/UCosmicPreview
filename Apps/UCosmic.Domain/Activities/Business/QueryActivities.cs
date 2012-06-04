using System;
using System.Linq;

namespace UCosmic.Domain.Activities
{
    internal static class QueryActivities
    {
        internal static IQueryable<Activity> WithPersonId(this IQueryable<Activity> queryable, int personId)
        {
            queryable = queryable.Where(a => a.PersonId == personId);
            return queryable;
        }

        internal static IQueryable<Activity> ByUserName(this IQueryable<Activity> queryable, string userName)
        {
            return queryable.Where(
                a => 
                a.Person.User != null && 
                a.Person.User.Name.Equals(userName, StringComparison.OrdinalIgnoreCase)
            );
        }

        internal static Activity ByUserNameAndNumber(this IQueryable<Activity> queryable, string userName, int number)
        {
            return queryable.SingleOrDefault(
                a =>
                a.Person.User != null &&
                a.Person.User.Name.Equals(userName, StringComparison.OrdinalIgnoreCase) &&
                a.Number == number
            );
        }
    }
}
