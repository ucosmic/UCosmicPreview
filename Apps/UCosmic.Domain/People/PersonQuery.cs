using System;
using System.Security.Principal;

namespace UCosmic.Domain.People
{
    public class PersonQuery : RevisableEntityQueryCriteria<Person>
    {
        public IPrincipal Principal { get; set; }
        //public Guid? EmailEntityId { get; set; }
        public string EmailAddress { get; set; }
        public Guid? EmailConfirmationToken { get; set; }
        public string EmailConfirmationIntent { get; set; }
        public string AutoCompleteFirstNamePrefix { get; set; }
        public string AutoCompleteLastNamePrefix { get; set; }
        public string AutoCompleteEmailTerm { get; set; }
    }

    public static class PersonBy
    {
        public static PersonQuery Principal(IPrincipal principal)
        {
            return new PersonQuery { Principal = principal };
        }

        public static PersonQuery EmailAddress(string emailAddress)
        {
            return new PersonQuery { EmailAddress = emailAddress };
        }

        public static PersonQuery EmailConfirmation(Guid token, string intent)
        {
            return new PersonQuery { EmailConfirmationToken = token, EmailConfirmationIntent = intent };
        }

        //public static PersonQuery EmailEntityId(Guid emailEntityId)
        //{
        //    return new PersonQuery { EmailEntityId = emailEntityId };
        //}
    }

    public static class PeopleWith
    {
        public static PersonQuery AutoCompleteFirstNamePrefix(string firstNamePrefix)
        {
            return new PersonQuery { AutoCompleteFirstNamePrefix = firstNamePrefix };
        }

        public static PersonQuery AutoCompleteLastNamePrefix(string lastNamePrefix)
        {
            return new PersonQuery { AutoCompleteLastNamePrefix = lastNamePrefix };
        }

        public static PersonQuery AutoCompleteEmailTerm(string emailTerm)
        {
            return new PersonQuery { AutoCompleteEmailTerm = emailTerm };
        }
    }
}