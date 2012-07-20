using System;
using System.Linq;
using System.Linq.Expressions;
using UCosmic.Domain;
using UCosmic.Domain.Activities;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Files;
using UCosmic.Domain.Identity;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Domain.Languages;
using UCosmic.Domain.People;
using UCosmic.Domain.Places;

namespace UCosmic
{
    public interface IQueryEntities
    {
        IQueryable<TEntity> Get<TEntity>() where TEntity : Entity;

        IQueryable<Language> Languages { get; }

        IQueryable<LooseFile> Files { get; }

        IQueryable<Place> Places { get; }

        IQueryable<GeoNamesToponym> GeoNamesToponyms { get; }
        IQueryable<GeoNamesFeatureClass> GeoNamesFeatureClasses { get; }
        IQueryable<GeoNamesFeature> GeoNamesFeatures { get; }
        IQueryable<GeoNamesTimeZone> GeoNamesTimeZones { get; }

        IQueryable<GeoPlanetPlace> GeoPlanetPlaces { get; }
        IQueryable<GeoPlanetPlaceType> GeoPlanetPlaceTypes { get; }

        IQueryable<User> Users { get; }
        IQueryable<Role> Roles { get; }

        IQueryable<Person> People { get; }
        IQueryable<Establishment> Establishments { get; }
        IQueryable<EstablishmentType> EstablishmentTypes { get; }
        IQueryable<EmailTemplate> EmailTemplates { get; }

        IQueryable<InstitutionalAgreement> InstitutionalAgreements { get; }
        IQueryable<InstitutionalAgreementConfiguration> InstitutionalAgreementConfigurations { get; }

        IQueryable<Activity> Activities { get; }

        TEntity FindByPrimaryKey<TEntity>(IQueryable<TEntity> entitiyQuery, params object[] primaryKeyValues)
            where TEntity : Entity;

        IQueryable<TEntity> EagerLoad<TEntity>(IQueryable<TEntity> query, Expression<Func<TEntity, object>> expression)
            where TEntity : Entity;

        //IQueryable<TEntity> ApplyInsertOrUpdate<TEntity>(IQueryable<TEntity> query, EntityQueryCriteria<TEntity> criteria)
        //    where TEntity : Entity;

        //IQueryable<TEntity> ApplyEagerLoading<TEntity>(IQueryable<TEntity> query, EntityQueryCriteria<TEntity> criteria)
        //    where TEntity : Entity;

        //IQueryable<TEntity> WithoutUnitOfWork<TEntity>(IQueryable<TEntity> query)
        //    where TEntity : Entity;
    }
}
