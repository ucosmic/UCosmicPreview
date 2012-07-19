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
using UCosmic.Domain.Activities;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Files;
using UCosmic.Domain.Identity;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Domain.Languages;
using UCosmic.Domain.People;
using UCosmic.Domain.Places;

namespace UCosmic.Impl.Orm
{
    public class UCosmicContext : DbContext, IUnitOfWork, ICommandEntities
    {
        public UCosmicContext(IDatabaseInitializer<UCosmicContext> initializer)
        {
            // inject initializer if passed
            if (initializer != null)
                Database.SetInitializer(initializer);
        }

        public IDbSet<LooseFile> Files { get; set; }
        IQueryable<LooseFile> IQueryEntities.Files { get { return Files; } }

        public IDbSet<Language> Languages { get; set; }
        IQueryable<Language> IQueryEntities.Languages { get { return Languages; } }

        public IDbSet<Place> Places { get; set; }
        IQueryable<Place> IQueryEntities.Places { get { return Places; } }

        public IDbSet<GeoNamesToponym> GeoNamesToponyms { get; set; }
        public IDbSet<GeoNamesFeature> GeoNamesFeatures { get; set; }
        public IDbSet<GeoNamesFeatureClass> GeoNamesFeatureClasses { get; set; }
        public IDbSet<GeoNamesTimeZone> GeoNamesTimeZones { get; set; }
        IQueryable<GeoNamesToponym> IQueryEntities.GeoNamesToponyms { get { return GeoNamesToponyms; } }
        IQueryable<GeoNamesFeatureClass> IQueryEntities.GeoNamesFeatureClasses { get { return GeoNamesFeatureClasses; } }
        IQueryable<GeoNamesFeature> IQueryEntities.GeoNamesFeatures { get { return GeoNamesFeatures; } }
        IQueryable<GeoNamesTimeZone> IQueryEntities.GeoNamesTimeZones { get { return GeoNamesTimeZones; } }

        public IDbSet<GeoPlanetPlace> GeoPlanetPlaces { get; set; }
        public IDbSet<GeoPlanetPlaceType> GeoPlanetPlaceTypes { get; set; }
        IQueryable<GeoPlanetPlace> IQueryEntities.GeoPlanetPlaces { get { return GeoPlanetPlaces; } }
        IQueryable<GeoPlanetPlaceType> IQueryEntities.GeoPlanetPlaceTypes { get { return GeoPlanetPlaceTypes; } }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        IQueryable<User> IQueryEntities.Users { get { return Users; } }
        IQueryable<Role> IQueryEntities.Roles { get { return Roles; } }

        public IDbSet<Establishment> Establishments { get; set; }
        public IDbSet<EstablishmentType> EstablishmentTypes { get; set; }
        public IDbSet<EmailTemplate> EmailTemplates { get; set; }
        IQueryable<Establishment> IQueryEntities.Establishments { get { return Establishments; } }
        IQueryable<EstablishmentType> IQueryEntities.EstablishmentTypes { get { return EstablishmentTypes; } }
        IQueryable<EmailTemplate> IQueryEntities.EmailTemplates { get { return EmailTemplates; } }

        public IDbSet<Person> People { get; set; }
        IQueryable<Person> IQueryEntities.People { get { return People; } }

        public IDbSet<InstitutionalAgreement> InstitutionalAgreements { get; set; }
        public IDbSet<InstitutionalAgreementConfiguration> InstitutionalAgreementConfigurations { get; set; }
        IQueryable<InstitutionalAgreement> IQueryEntities.InstitutionalAgreements { get { return InstitutionalAgreements; } }
        IQueryable<InstitutionalAgreementConfiguration> IQueryEntities.InstitutionalAgreementConfigurations
        {
            get { return InstitutionalAgreementConfigurations; }
        }

        public IDbSet<Activity> Activities { get; set; }
        IQueryable<Activity> IQueryEntities.Activities { get { return Activities; } }

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

        public void Create(Entity entity)
        {
            var entry = Entry(entity);
            entry.State = EntityState.Added;
        }

        public void Update(Entity entity)
        {
            var entry = Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void Purge(Entity entity)
        {
            var entry = Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public IQueryable<TEntity> EagerLoad<TEntity>(IQueryable<TEntity> query, Expression<Func<TEntity, object>> expression)
            where TEntity : Entity
        {
            if (query != null && expression != null)
                query = query.Include(expression);
            return query;
        }

        public IQueryable<TEntity> WithoutUnitOfWork<TEntity>(IQueryable<TEntity> query)
            where TEntity : Entity
        {
            if (query != null)
                query = query.AsNoTracking();
            return query;
        }

        public IQueryable<TEntity> ApplyEagerLoading<TEntity>(IQueryable<TEntity> query, EntityQueryCriteria<TEntity> criteria)
            where TEntity : Entity
        {
            if (query != null && criteria != null && criteria.ToBeEagerLoaded != null && criteria.ToBeEagerLoaded.Count > 0)
                query = criteria.ToBeEagerLoaded.Aggregate(query, (lastInclude, nextInclude) =>
                    lastInclude.Include(nextInclude));
            return query;
        }

        public TEntity FindByPrimaryKey<TEntity>(IQueryable<TEntity> entitiyQuery, params object[] primaryKeyValues)
            where TEntity : Entity
        {
            var dbSet = (IDbSet<TEntity>)entitiyQuery;
            return dbSet.Find(primaryKeyValues);
        }

        public IQueryable<TEntity> ApplyInsertOrUpdate<TEntity>(IQueryable<TEntity> query, EntityQueryCriteria<TEntity> criteria)
            where TEntity : Entity
        {
            if (query != null && criteria != null && criteria.IsForInsertOrUpdate)
                return query.AsNoTracking();
            return query;
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