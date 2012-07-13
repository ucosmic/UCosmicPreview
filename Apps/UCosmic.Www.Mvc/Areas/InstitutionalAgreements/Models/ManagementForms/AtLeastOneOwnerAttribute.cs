using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;
using UCosmic.IoC;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AtLeastOneOwnerAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var participantForms = value as IEnumerable<InstitutionalAgreementParticipantForm>;
            if (participantForms != null)
            {
                // find out whether an owning establishment is affiliated with current user
                var ownerForms = participantForms.Where(p => p.IsOwner && !p.IsDeleted).ToList();
                if (ownerForms.Count > 0)
                {
                    var participantIds = ownerForms.Select(form => form.EstablishmentEntityId).ToList();
                    var entityQueries = DependencyInjector.Current.GetService<IQueryEntities>();
                    var establishments = new EstablishmentFinder(entityQueries);

                    Expression<Func<Affiliation, bool>> myDefaultAffiliation = affiliation =>
                        affiliation.IsDefault && affiliation.Person.User != null
                        && affiliation.Person.User.Name.Equals(Thread.CurrentPrincipal.Identity.Name, StringComparison.OrdinalIgnoreCase);

                    var owners = establishments.FindMany(With<Establishment>.EntityIds(participantIds))
                        .Where(e => e.Affiliates.AsQueryable().Any(myDefaultAffiliation)
                                    || e.Ancestors.Any(a => a.Ancestor.Affiliates.AsQueryable().Any(myDefaultAffiliation)));

                    if (owners.Any()) return true;
                }
                return false;
            }

            // if there are no participants, none of them can be the owner
            if (value == null)
                return false;

            throw new NotSupportedException(
                "AtLeastOneOwner ValidationAttribute can only operate on instances of IEnumerable<InstitutionalAgreementParticipantForm>.");
        }
    }
}