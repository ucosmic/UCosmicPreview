using System;

namespace UCosmic.Domain.Identity
{
    public class GetUserByEntityIdQuery : BaseUserQuery, IDefineQuery<User>
    {
        public Guid EntityId { get; set; }
    }
}
