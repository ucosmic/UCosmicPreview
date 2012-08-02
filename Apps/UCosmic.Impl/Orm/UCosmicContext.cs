using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Domain.People;

namespace UCosmic.Impl.Orm
{
    public class UCosmicContext : DbContext, IUnitOfWork, ICommandEntities
    {
        public UCosmicContext(
            IDatabaseInitializer<UCosmicContext> initializer
        )
        {
            // inject initializer if passed
            if (initializer != null)
                Database.SetInitializer(initializer);
        }

        //public IDbSet<Language> Languages { get; set; }
        //public IDbSet<Place> Places { get; set; }
        //public IDbSet<User> Users { get; set; }
        //public IDbSet<Role> Roles { get; set; }
        public IDbSet<Establishment> Establishments { get; set; }
        //public IDbSet<EmailTemplate> EmailTemplates { get; set; }
        public IDbSet<Person> People { get; set; }
        public IDbSet<InstitutionalAgreement> InstitutionalAgreements { get; set; }
        public IDbSet<InstitutionalAgreementConfiguration> InstitutionalAgreementConfigurations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            FilesRelationalMapper.AddConfigurations(modelBuilder);
            LanguagesRelationalMapper.AddConfigurations(modelBuilder);

            PlacesRelationalMapper.AddConfigurations(modelBuilder);
            GeoPlanetRelationalMapper.AddConfigurations(modelBuilder);
            GeoNamesRelationalMapper.AddConfigurations(modelBuilder);

            IdentityRelationalMapper.AddConfigurations(modelBuilder);
            PeopleRelationalMapper.AddConfigurations(modelBuilder);
            EstablishmentsRelationalMapper.AddConfigurations(modelBuilder);

            InstitutionalAgreementsRelationalMapper.AddConfigurations(modelBuilder);

            ActivitiesRelationalMapper.AddConfigurations(modelBuilder);
        }

        public IQueryable<TEntity> Get<TEntity>() where TEntity : Entity
        {
            return Set<TEntity>();
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : Entity
        {
            return Set<TEntity>().AsNoTracking();
        }

        public TEntity FindByPrimaryKey<TEntity>(params object[] primaryKeyValues)
            where TEntity : Entity
        {
            return Set<TEntity>().Find(primaryKeyValues);
        }

        public IQueryable<TEntity> EagerLoad<TEntity>(IQueryable<TEntity> query, Expression<Func<TEntity, object>> expression)
            where TEntity : Entity
        {
            if (query != null && expression != null)
                query = query.Include(expression);
            return query;
        }

        public void Create<TEntity>(TEntity entity) where TEntity: Entity
        {
            if (Entry(entity).State == EntityState.Detached)
                Set<TEntity>().Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : Entity
        {
            var entry = Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void Purge<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (Entry(entity).State != EntityState.Deleted)
                Set<TEntity>().Remove(entity);
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch(DbEntityValidationException ex)
            {
                var config = new DotNetConfigurationManager();
                if (!config.IsDeployedToCloud) throw;
                var mailSender = new SmtpMailSender(config, new ElmahExceptionLogger(config));
                var message = new MailMessage(config.EmailDefaultFromAddress, config.EmailEmergencyAddresses)
                {
                    Subject = "UCosmic caught DbEntityValidationException during SaveChanges.",
                };
                var body = new StringBuilder();
                foreach (var entry in ex.EntityValidationErrors)
                {
                    var entityType = entry.Entry.Entity.GetType();
                    foreach (var error in entry.ValidationErrors)
                    {
                        body.Append(entityType.Name);
                        body.Append('.');
                        body.Append(error.PropertyName);
                        body.Append(": \r\n");
                        body.Append(error.ErrorMessage);
                        body.Append("\r\n\r\n");
                    }
                }
                message.Body = body.ToString();
                mailSender.Send(message);
                throw;
            }
        }
    }
}