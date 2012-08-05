using System.Linq;
using System.Web.Security;
using UCosmic.Domain.Identity;

namespace UCosmic.Impl.Seeders2
{
    public class MemberEntitySeeder : BaseDataSeeder
    {
        private readonly IQueryEntities _entities;

        public MemberEntitySeeder(IQueryEntities entities)
        {
            _entities = entities;
        }

        public override void Seed()
        {
            // delete all app services db users
            var members = Membership.GetAllUsers();
            foreach (var member in members.Cast<MembershipUser>())
                Membership.DeleteUser(member.UserName, true);

            // create new app services db user for each registered user
            var users = _entities.Query<User>().Where(u => u.IsRegistered && u.EduPersonTargetedId == null);
            foreach (var user in users)
            {
                Membership.CreateUser(user.Name, "asdfasdf", user.Person.DefaultEmail.Value);
            }
        }
    }
}
