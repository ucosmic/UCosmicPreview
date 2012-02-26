namespace UCosmic.Domain.Identity
{
    public class RoleQuery : RevisableEntityQueryCriteria<Role>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string GrantToUser { get; set; }
    }

    public static class RoleBy
    {
        public static RoleQuery Name(string name)
        {
            return new RoleQuery { Name = name };
        }

        public static RoleQuery Slug(string slug)
        {
            return new RoleQuery { Slug = slug };
        }
    }

    public static class RolesWith
    {
        public static RoleQuery GrantToUser(string userName)
        {
            return new RoleQuery { GrantToUser = userName };
        }
    }
}