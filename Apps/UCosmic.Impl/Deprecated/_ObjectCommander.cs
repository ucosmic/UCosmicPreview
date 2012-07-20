//using System.Data;
//using UCosmic.Domain;
//using System.Data.Entity.Validation;

//namespace UCosmic.Impl.Orm
//{
//    public class ObjectCommander : ICommandObjects
//    {
//        private IUnitOfWork UnitOfWork { get; set; }
//        private UCosmicContext DbContext
//        {
//            get { return UnitOfWork as UCosmicContext; }
//        }

//        public ObjectCommander(IUnitOfWork unitOfWork)
//        {
//            UnitOfWork = unitOfWork;
//        }

//        public int Insert(object entity, bool saveChanges = false)
//        {
//            var dbEntityEntry = DbContext.Entry(entity);
//            dbEntityEntry.State = EntityState.Added;
//            return (saveChanges) ? SaveChanges() : 0;
//        }

//        public int Update(object entity, bool saveChanges = false)
//        {
//            var dbEntityEntry = DbContext.Entry(entity);
//            dbEntityEntry.State = EntityState.Modified;
//            return (saveChanges) ? SaveChanges() : 0;
//        }

//        public int Delete(object entity, bool saveChanges = false)
//        {
//            var dbEntityEntry = DbContext.Entry(entity);
//            dbEntityEntry.State = EntityState.Deleted;
//            return (saveChanges) ? SaveChanges() : 0;
//        }

//        public int SaveChanges()
//        {
//            try
//            {
//                return UnitOfWork.SaveChanges();
//            }
//            catch (DbEntityValidationException ex)
//            {
//                // ReSharper disable UnusedVariable
//                var validationErrors = ex.EntityValidationErrors;
//                // ReSharper restore UnusedVariable
//                throw;
//            }
//        }

//        public void Dispose()
//        {
//            UnitOfWork.Dispose();
//        }
//    }
//}