using System;
using System.Text;

namespace UCosmic.Domain.People
{
    public class PersonFactory
    {
        public static string DeriveDisplayName(string lastName, string firstName, string middleName, string salutation, string suffix)
        {
            var displayName = new StringBuilder(lastName);
            if (!string.IsNullOrWhiteSpace(middleName))
                displayName.Insert(0, string.Format("{0} ", middleName));

            if (!string.IsNullOrWhiteSpace(firstName))
                displayName.Insert(0, string.Format("{0} ", firstName));

            if (!string.IsNullOrWhiteSpace(salutation))
                displayName.Insert(0, string.Format("{0} ", salutation));

            if (!string.IsNullOrWhiteSpace(suffix))
                displayName.Append(string.Format(" {0}", suffix));


            return displayName.ToString().Trim();
        }

        public static string DeriveDisplayName(Person person)
        {
            if (person == null) throw new ArgumentNullException("person");
            return person.IsDisplayNameDerived
                ? DeriveDisplayName(person.LastName, person.FirstName, 
                    person.MiddleName, person.Salutation, person.Suffix)
                : person.DisplayName;
        }
    }
}
