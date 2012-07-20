//using System.Linq;
//using Moq;
//using UCosmic.Domain.Establishments;
//using UCosmic.Domain.People;

//namespace UCosmic.Domain
//{
//    public static class InterfaceQueryEntitiesExtensions
//    {
//        //public static Mock<IQueryEntities> Initialize(this Mock<IQueryEntities> mock)
//        //{
//        //    mock.SetupApplyMethods<Person>();
//        //    mock.SetupApplyMethods<Establishment>();
//        //    mock.SetupApplyMethods<EmailTemplate>();
//        //    return mock;
//        //}

//        //private static void SetupApplyMethods<TEntity>(this Mock<IQueryEntities> mock)
//        //    where TEntity : Entity
//        //{
//        //    mock.Setup(m => m.ApplyInsertOrUpdate(It.IsAny<IQueryable<TEntity>>(), It.IsAny<EntityQueryCriteria<TEntity>>()))
//        //        .Returns((IQueryable<TEntity> arg0, EntityQueryCriteria<TEntity> arg1) => FuncApply(arg0, arg1));
//        //    mock.Setup(m => m.ApplyEagerLoading(It.IsAny<IQueryable<TEntity>>(), It.IsAny<EntityQueryCriteria<TEntity>>()))
//        //        .Returns((IQueryable<TEntity> arg0, EntityQueryCriteria<TEntity> arg1) => FuncApply(arg0, arg1));
//        //}

//        //// ReSharper disable UnusedParameter.Local
//        //private static IQueryable<TEntity> FuncApply<TEntity>(IQueryable<TEntity> queryable, EntityQueryCriteria<TEntity> criteria)
//        //// ReSharper restore UnusedParameter.Local
//        //{
//        //    return queryable;
//        //}
//    }
//}