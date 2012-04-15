//using System;
//using System.Linq;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using UCosmic.Domain.Establishments;
//using UCosmic.Domain.People;

//namespace UCosmic.Domain.Identity
//{
//    public class UserFacade : RevisableEntityFacade<User>
//    {
//        private readonly EstablishmentFinder _establishmentFinder;
//        private readonly PersonFinder _personFinder;

//        public UserFacade(ICommandEntities entities
//            , EstablishmentFinder establishmentFinder
//            , PersonFinder personFinder
//        ) : base(entities)
//        {
//            _establishmentFinder = establishmentFinder;
//            _personFinder = personFinder;
//        }

//        public virtual IEnumerable<User> Get(params Expression<Func<User, object>>[] eagerLoads)
//        {
//            return Get(Entities.Users, eagerLoads);
//        }

//        public User Get(string name, params Expression<Func<User, object>>[] eagerLoads)
//        {
//            if (name == null) throw new ArgumentNullException("name");
//            var query = EagerLoad(Entities.Users, eagerLoads);
//            var user = query.ByName(name);
//            return user;
//        }

//        public User Get(Guid entityId, params Expression<Func<User, object>>[] eagerLoads)
//        {
//            return Get(Entities.Users, entityId, eagerLoads);
//        }

//        public User Get(int revisionId, params Expression<Func<User, object>>[] eagerLoads)
//        {
//            return Get(Entities.Users, revisionId, eagerLoads);
//        }

//        //public User GetBySubjectNameId(string subjectNameId, params Expression<Func<User, object>>[] eagerLoads)
//        //{
//        //    var query = EagerLoad(Entities.Users, eagerLoads);
//        //    var user = query.BySubjectNameId(subjectNameId);
//        //    return user;
//        //}

//        //public IEnumerable<User> AutoComplete(string term, IEnumerable<Guid> excludeEntityIds)
//        //{
//        //    if (term == null) throw new ArgumentNullException("term");

//        //    var query = Entities.Users
//        //        .Exclude(excludeEntityIds)
//        //        .AutoComplete(term)
//        //        .OrderBy(u => u.Name)
//        //    ;
//        //    return query;
//        //}

//        //public User GetOrCreate(string userName, bool isRegistered, string subjectNameId = null)
//        //{
//        //    // first see if user exists
//        //    var user = Get(userName) ?? Get(subjectNameId);

//        //    if (user == null)
//        //    {
//        //        user = new User
//        //        {
//        //            Name = userName,
//        //            IsRegistered = isRegistered,
//        //            //SubjectNameId = subjectNameId,
//        //            //Person = _personFinder.FindOne(PersonBy.EmailAddress(userName))
//        //            //            ?? PersonFactory.Create(userName),
//        //        };

//        //        var establishment = _establishmentFinder.FindOne(EstablishmentBy.EmailDomain(userName));
//        //        if (establishment != null)
//        //            user.Person.AffiliateWith(establishment);

//        //        var email = user.Person.Emails.ByValue(userName);
//        //        if (isRegistered && !email.IsConfirmed) email.IsConfirmed = true;

//        //        Entities.Create(user);
//        //    }

//        //    // make sure user has correct registration
//        //    if (!isRegistered.Equals(user.IsRegistered))
//        //        user.IsRegistered = isRegistered;

//        //    //// make sure user has correct subject name id
//        //    //if (!string.IsNullOrWhiteSpace(subjectNameId) && !subjectNameId.Equals(user.SubjectNameId))
//        //    //    user.SubjectNameId = subjectNameId;

//        //    return user;
//        //}
//    }
//}
