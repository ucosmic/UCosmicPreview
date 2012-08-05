using System.Linq;
using System.Web.Security;

namespace UCosmic.Impl.Seeders
{
    public class CompositeSqlSeeder : BaseDataSeeder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DevelopmentSqlSeeder _sqlSeeder;
        private readonly MemberEntitySeeder _memberEntitySeeder;

        public CompositeSqlSeeder(IUnitOfWork unitOfWork
            , DevelopmentSqlSeeder sqlSeeder
            , MemberEntitySeeder memberEntitySeeder
        )
        {
            _unitOfWork = unitOfWork;
            _sqlSeeder = sqlSeeder;
            _memberEntitySeeder = memberEntitySeeder;
        }

        public override void Seed()
        {
            _sqlSeeder.Seed();
            _unitOfWork.SaveChanges();

            var members = Membership.GetAllUsers().Cast<MembershipUser>();
            if (!members.Any()) _memberEntitySeeder.Seed();
        }
    }
}
