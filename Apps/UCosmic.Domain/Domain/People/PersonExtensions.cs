using System.Linq;
using System.Threading;

namespace UCosmic.Domain.People
{
    public static class PersonExtensions
    {
        public static Person ForThreadPrincipal(this IQueryable<Person> query)
        {
            return (query != null)
                       ? query.Current().SingleOrDefault(p =>
                                                         p.User != null && p.User.Name.Equals(Thread.CurrentPrincipal.Identity.Name))
                       : null;
        }
    }
}