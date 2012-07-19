//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace UCosmic.Domain.Establishments
//{
//    public class EstablishmentTypeFinder : RevisableEntityFinder<EstablishmentType>
//    {
//        public EstablishmentTypeFinder(IQueryEntities entityQueries)
//            : base(entityQueries)
//        {
//        }

//        public override ICollection<EstablishmentType> FindMany(RevisableEntityQueryCriteria<EstablishmentType> criteria)
//        {
//            var query = InitializeQuery(EntityQueries.EstablishmentTypes, criteria);
//            var finder = criteria as EstablishmentTypeQuery ?? new EstablishmentTypeQuery();

//            // apply category code
//            if (!string.IsNullOrWhiteSpace(finder.CategoryCode))
//                query = query.Where(t => finder.CategoryCode.Equals(t.Category.Code, StringComparison.OrdinalIgnoreCase));

//            // apply English Name
//            if (!string.IsNullOrWhiteSpace(finder.EnglishName))
//                return new[] { query.SingleOrDefault(e => finder.EnglishName.Equals(e.EnglishName)) };

//            query = FinalizeQuery(query, criteria);

//            var results = query.ToList();
//            return results;
//        }
//    }
//}
