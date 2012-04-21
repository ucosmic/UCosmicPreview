using System.Linq;
using System.Security.Principal;
using Moq;
using UCosmic.Domain.Email;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;

namespace UCosmic.Domain
{
    public static class InterfaceQueryEntitiesExtensions
    {
        public static Mock<IQueryEntities> Initialize(this Mock<IQueryEntities> mock)
        {
            mock.SetupApplyMethods<Person>();
            mock.SetupApplyMethods<Establishment>();
            mock.SetupApplyMethods<EmailTemplate>();
            return mock;
        }

        private static void SetupApplyMethods<TEntity>(this Mock<IQueryEntities> mock)
            where TEntity : Entity
        {
            mock.Setup(m => m.ApplyInsertOrUpdate(It.IsAny<IQueryable<TEntity>>(), It.IsAny<EntityQueryCriteria<TEntity>>()))
                .Returns((IQueryable<TEntity> arg0, EntityQueryCriteria<TEntity> arg1) => FuncApply(arg0, arg1));
            mock.Setup(m => m.ApplyEagerLoading(It.IsAny<IQueryable<TEntity>>(), It.IsAny<EntityQueryCriteria<TEntity>>()))
                .Returns((IQueryable<TEntity> arg0, EntityQueryCriteria<TEntity> arg1) => FuncApply(arg0, arg1));
        }

        public static IPrincipal AsPrincipal(this string principalIdentityName)
        {
            if (principalIdentityName == null)
            {
                var identity = new Mock<IIdentity>(MockBehavior.Strict);
                var principal = new Mock<IPrincipal>(MockBehavior.Strict);
                identity.Setup(p => p.Name).Returns(null as string);
                principal.Setup(p => p.Identity).Returns(identity.Object);
                return principal.Object;
            }
            else
            {
                var principal = new GenericPrincipal(new GenericIdentity(principalIdentityName), null);
                return principal;
            }
        }

        // ReSharper disable UnusedParameter.Local
        private static IQueryable<TEntity> FuncApply<TEntity>(IQueryable<TEntity> queryable, EntityQueryCriteria<TEntity> criteria)
        // ReSharper restore UnusedParameter.Local
        {
            return queryable;
        }

    }
}