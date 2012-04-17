
namespace UCosmic.Domain.Identity
{
    public class GetUserByEduPersonTargetedIdQuery : BaseUsersQuery, IDefineQuery<User>
    {
        public string EduPersonTargetedId { get; set; }
    }
}
