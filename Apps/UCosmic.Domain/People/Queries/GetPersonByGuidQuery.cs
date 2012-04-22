using System;

namespace UCosmic.Domain.People
{
    public class GetPersonByGuidQuery : BasePersonQuery, IDefineQuery<Person>
    {
        public Guid Guid { get; set; }
    }
}
