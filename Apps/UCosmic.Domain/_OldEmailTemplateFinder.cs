//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace UCosmic.Domain.Email
//{
//    public class EmailTemplateFinder : RevisableEntityFinder<EmailTemplate>
//    {
//        public EmailTemplateFinder(IQueryEntities entityQueries)
//            : base(entityQueries)
//        {
//        }

//        public override ICollection<EmailTemplate> FindMany(RevisableEntityQueryCriteria<EmailTemplate> criteria)
//        {
//            var query = InitializeQuery(EntityQueries.EmailTemplates, criteria);
//            var finder = criteria as EmailTemplateQuery ?? new EmailTemplateQuery();

//            // apply name
//            if (!string.IsNullOrWhiteSpace(finder.Name))
//                query = query.Where(e => finder.Name.Equals(e.Name, StringComparison.OrdinalIgnoreCase));

//            // apply establishment
//            if (finder.ForEstablishmentRevisionId.HasValue && finder.ForEstablishmentRevisionId.Value != 0)
//                if (finder.FallBackToDefault.HasValue && finder.FallBackToDefault.Value)
//                    query = query.Any(e => e.Establishment != null && e.Establishment.RevisionId == finder.ForEstablishmentRevisionId.Value)
//                        ? query.Where(e => e.Establishment != null && e.Establishment.RevisionId == finder.ForEstablishmentRevisionId.Value) 
//                        : query.Where(e => e.Establishment == null);
//                else
//                    query = query.Where(e => e.Establishment != null && e.Establishment.RevisionId == finder.ForEstablishmentRevisionId.Value);

//            query = FinalizeQuery(query, criteria);

//            var results = query.ToList();
//            return results;
//        }
//    }
//}