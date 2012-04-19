﻿using System.Text;

namespace UCosmic.Domain.People
{
    public class GenerateDisplayNameHandler : IHandleQueries<GenerateDisplayNameQuery, string>
    {
        public string Handle(GenerateDisplayNameQuery query)
        {
            var builder = new StringBuilder();

            // start with the last name
            if (!string.IsNullOrWhiteSpace(query.LastName))
                builder.Insert(0, query.LastName.Trim());

            // prefix with middle name if it exists
            if (!string.IsNullOrWhiteSpace(query.MiddleName))
                builder.Insert(0, string.Format("{0} ", query.MiddleName.Trim()));

            // prefix with first name if it exists
            if (!string.IsNullOrWhiteSpace(query.FirstName))
                builder.Insert(0, string.Format("{0} ", query.FirstName.Trim()));

            // prefix with salutation if it exists
            if (!string.IsNullOrWhiteSpace(query.Salutation))
                builder.Insert(0, string.Format("{0} ", query.Salutation.Trim()));

            // append suffix if it exists
            if (!string.IsNullOrWhiteSpace(query.Suffix))
                builder.Append(string.Format(" {0}", query.Suffix.Trim()));

            var displayName = builder.ToString().Trim();
            return string.IsNullOrWhiteSpace(displayName) ? null : displayName;
        }
    }
}
