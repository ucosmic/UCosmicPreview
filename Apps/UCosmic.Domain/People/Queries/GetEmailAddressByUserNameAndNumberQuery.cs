using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.People
{
    public class GetEmailAddressByUserNameAndNumberQuery : IDefineQuery<EmailAddress>
    {
        public string UserName { get; set; }
        public int Number { get; set; }
    }
}
