using System;

namespace UCosmic.Domain.Identity
{
    public class GetUserByEntityIdQuery : IDefineQuery<User>
    {
        public Guid EntityId { get; set; }
    }
}
