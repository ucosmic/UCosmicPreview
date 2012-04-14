namespace UCosmic.Domain.Identity
{
    public class GetUserByNameQuery : IDefineQuery<User>
    {
        public string Name { get; set; }
    }
}
