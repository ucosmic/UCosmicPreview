using System;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;

namespace UCosmic.Domain
{
    public static class EagerLoadingTestExtensions
    {
        public static Mock<IQueryEntities> Initialize(this Mock<IQueryEntities> mock)
        {
            mock.SetupApplyMethods<User>();
            mock.SetupApplyMethods<Person>();
            mock.SetupApplyMethods<EmailConfirmation>();
            mock.SetupApplyMethods<Affiliation>();
            mock.SetupApplyMethods<Establishment>();
            mock.SetupApplyMethods<EmailTemplate>();
            return mock;
        }

        private static void SetupApplyMethods<TEntity>(this Mock<IQueryEntities> mock)
            where TEntity : Entity
        {
            mock.Setup(m => m.EagerLoad(It.IsAny<IQueryable<TEntity>>(), It.IsAny<Expression<Func<TEntity, object>>>()))
                .Returns((IQueryable<TEntity> arg0, Expression<Func<TEntity, object>> arg1) => FuncApply(arg0, arg1));
        }

        public static Mock<ICommandEntities> Initialize(this Mock<ICommandEntities> mock)
        {
            mock.SetupApplyMethods<User>();
            mock.SetupApplyMethods<Person>();
            mock.SetupApplyMethods<EmailConfirmation>();
            mock.SetupApplyMethods<Affiliation>();
            mock.SetupApplyMethods<Establishment>();
            mock.SetupApplyMethods<EmailTemplate>();
            return mock;
        }

        private static void SetupApplyMethods<TEntity>(this Mock<ICommandEntities> mock)
            where TEntity : Entity
        {
            mock.Setup(m => m.EagerLoad(It.IsAny<IQueryable<TEntity>>(), It.IsAny<Expression<Func<TEntity, object>>>()))
                .Returns((IQueryable<TEntity> arg0, Expression<Func<TEntity, object>> arg1) => FuncApply(arg0, arg1));
        }

        // ReSharper disable UnusedParameter.Local
        private static IQueryable<TEntity> FuncApply<TEntity>(IQueryable<TEntity> queryable, Expression<Func<TEntity, object>> criteria)
        // ReSharper restore UnusedParameter.Local
        {
            return queryable;
        }
    }
}