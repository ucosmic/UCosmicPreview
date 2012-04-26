namespace UCosmic.Domain.Identity
{
    public class FindRolesGrantedToUserNameQuery : BaseUserQuery, IDefineQuery<Role[]>
    {
        public string UserName { get; set; }
    }
}
