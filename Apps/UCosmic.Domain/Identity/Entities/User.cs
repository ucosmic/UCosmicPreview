using System.Collections.Generic;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Identity
{
    public class User : RevisableEntity
    {
        protected internal User()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Grants = Grants ?? new List<RoleGrant>();
            SubjectNameIdentifiers = SubjectNameIdentifiers ?? new List<SubjectNameIdentifier>();
            EduPersonScopedAffiliations = EduPersonScopedAffiliations ?? new List<EduPersonScopedAffiliation>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public string Name { get; protected internal set; }

        public string EduPersonTargetedId { get; protected internal set; }

        public bool IsRegistered { get; protected internal set; }

        public virtual ICollection<RoleGrant> Grants { get; protected internal set; }

        public virtual ICollection<SubjectNameIdentifier> SubjectNameIdentifiers { get; protected set; }

        public virtual ICollection<EduPersonScopedAffiliation> EduPersonScopedAffiliations { get; protected set; }

        public virtual Person Person { get; protected internal set; }

        //internal void LogSubjectNameIdentifier(string subjectNameIdentifier)
        //{
        //    // first check whether the subject name identifier already exists
        //    var subjectNameIdentifierEntity = SubjectNameIdentifiers
        //        .SingleOrDefault(s => s.Value == subjectNameIdentifier);

        //    // if it already exists, update it
        //    if (subjectNameIdentifierEntity != null)
        //        subjectNameIdentifierEntity.UpdatedOnUtc = DateTime.UtcNow;

        //    // otherwise, add it to the collection
        //    else
        //        SubjectNameIdentifiers.Add(new SubjectNameIdentifier
        //        {
        //            Value = subjectNameIdentifier,
        //            Number = SubjectNameIdentifiers.NextNumber(),
        //        });
        //}

        //internal void SetEduPersonScopedAffiliations(string[] eduPersonScopedAffiliations)
        //{
        //    // remove all previous affiliations
        //    EduPersonScopedAffiliations.Clear();

        //    // add each new affiliation
        //    if (eduPersonScopedAffiliations != null)
        //        foreach (var eduPersonScopedAffiliation in eduPersonScopedAffiliations
        //            .Where(eduPersonScopedAffiliation => !string.IsNullOrWhiteSpace(eduPersonScopedAffiliation)))
        //                EduPersonScopedAffiliations.Add(new EduPersonScopedAffiliation
        //                    {
        //                        Value = eduPersonScopedAffiliation,
        //                        Number = EduPersonScopedAffiliations.NextNumber(),
        //                    });
        //}

        public bool IsInRole(string roleName)
        {
            return Grants.ByRole(roleName) != null;
        }
    }

}