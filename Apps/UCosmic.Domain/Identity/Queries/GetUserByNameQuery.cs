
namespace UCosmic.Domain.Identity
{
    public class GetUserByNameQuery : BaseUserQuery, IDefineQuery<User>
    {
        public string Name { get; set; }
    }
}
