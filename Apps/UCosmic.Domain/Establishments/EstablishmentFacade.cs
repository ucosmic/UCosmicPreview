using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentFacade : RevisableEntityFacade<Establishment>
    {
        public EstablishmentFacade(ICommandEntities entities) : base(entities)
        {
        }

        //public IEnumerable<Establishment> GetSamlIntegrated(params Expression<Func<Establishment, object>>[] eagerLoads)
        //{
        //    var query = EagerLoad(Entities.Establishments, eagerLoads);
        //    return query.SamlIntegrated().OrderBy(e => e.OfficialName);
        //}

        //public EstablishmentSamlSignOn GetSamlSignOnFor(string emailAddress)
        //{
        //    var establishment = GetByEmail(emailAddress);
        //    EstablishmentSamlFactory.EnsureMetadataIsCached(establishment.SamlSignOn);
        //    return establishment.SamlSignOn;
        //}

        public bool IsIssuerTrusted(string issuerNameIdentifier)
        {
            var establishment = GetBySamlEntityId(issuerNameIdentifier);
            return (establishment != null && establishment.SamlSignOn != null);
        }

        //public Establishment GetByEmail(string email, params Expression<Func<Establishment, object>>[] eagerLoads)
        //{
        //    var query = EagerLoad(Entities.Establishments, eagerLoads);
        //    return query.ByEmail(email);
        //}

        public Establishment GetBySamlEntityId(string samlEntityId, params Expression<Func<Establishment, object>>[] eagerLoads)
        {
            var query = EagerLoad(Entities.Establishments, eagerLoads);
            return query.BySamlEntityId(samlEntityId);
        }

    }
}