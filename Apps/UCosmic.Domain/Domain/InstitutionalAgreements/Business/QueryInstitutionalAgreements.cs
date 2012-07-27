using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using LinqKit;
using UCosmic.Domain.People;

namespace UCosmic.Domain.InstitutionalAgreements
{
    internal static class QueryInstitutionalAgreements
    {
        internal static IQueryable<InstitutionalAgreement> IsRoot(this IQueryable<InstitutionalAgreement> queryable)
        {
            return queryable.Where(a => a.Umbrella == null);
        }

        internal static IQueryable<InstitutionalAgreement> WithAnyChildren(this IQueryable<InstitutionalAgreement> queryable)
        {
            return queryable.Where(a => a.Children.Any());
        }

        internal static IQueryable<InstitutionalAgreement> ForTenantUser(this IQueryable<InstitutionalAgreement> queryable, IPrincipal principal)
        {
            // select all agreements where this user's default affiliation is the owner
            Expression<Func<Affiliation, bool>> principalDefaultAffiliation =
                affiliation =>
                affiliation.IsDefault &&
                affiliation.Person.User != null &&
                affiliation.Person.User.Name.Equals(principal.Identity.Name, StringComparison.OrdinalIgnoreCase);

            return queryable.Where
            (
                agreement =>
                agreement.Participants.Any
                (
                    participant =>

                    // only get owning participants
                    participant.IsOwner &&
                    (
                        // where the owning participant is user's default establishment
                        participant.Establishment.Affiliates.AsQueryable().Any(principalDefaultAffiliation) ||

                        // or the owning participant's ancestor is user's default establishment
                        participant.Establishment.Ancestors.Any
                        (
                            ancestor =>
                            ancestor.Ancestor.Affiliates.AsQueryable().Any(principalDefaultAffiliation)
                        )
                    )
                )
            );
        }

        internal static IQueryable<InstitutionalAgreement> UmbrellaCandidatesFor(this IQueryable<InstitutionalAgreement> queryable, int institutionalAgreementId)
        {
            // candidates cannot be self or offspring of requesting agreement
            return queryable.Where
            (
                a =>
                a.RevisionId != institutionalAgreementId &&
                a.Ancestors.All(h => h.Ancestor.RevisionId != institutionalAgreementId)
            );
        }

        internal static InstitutionalAgreement ByFileGuid(this IQueryable<InstitutionalAgreement> queryable, Guid guid)
        {
            return queryable.SingleOrDefault(a => a.Files.Any(f => f.EntityId == guid));
        }

        internal static IQueryable<InstitutionalAgreement> OwnedByEstablishment(this IQueryable<InstitutionalAgreement> queryable, object establishmentKey)
        {
            queryable = queryable.Where(IsOwnedBy(establishmentKey));
            return queryable;
        }

        private static Expression<Func<InstitutionalAgreement, bool>> IsOwnedBy(object establishmentKey)
        {
            #region where participant owns the agreement, and id matches participant or one of its ancestors

            if (establishmentKey is int)
                return
                    agreement =>
                    agreement.Participants.Any
                    (
                        participant =>
                        participant.IsOwner // participant owns the agreement, and
                        &&
                        (
                            // either the participant id matches,
                            participant.Establishment.RevisionId == (int)establishmentKey
                            ||
                            // or the participant has ancestors whose id matches
                            participant.Establishment.Ancestors.Any
                            (
                                hierarchy =>
                                hierarchy.Ancestor.RevisionId == (int)establishmentKey
                            )
                        )
                    );
            if (establishmentKey is Guid)
                return
                    agreement =>
                    agreement.Participants.Any
                    (
                        participant =>
                        participant.IsOwner // participant owns the agreement, and
                        &&
                        (
                            // either the participant id matches,
                            participant.Establishment.EntityId == (Guid)establishmentKey
                            ||
                            // or the participant has ancestors whose id matches
                            participant.Establishment.Ancestors.Any
                            (
                                hierarchy =>
                                hierarchy.Ancestor.EntityId == (Guid)establishmentKey
                            )
                        )
                    );
            var key = establishmentKey as string;
            if (key != null)
                return
                    agreement =>
                    agreement.Participants.Any
                    (
                        participant =>
                        participant.IsOwner // participant owns the agreement, and
                        &&
                        (
                            // either the participant id matches,
                            key.Equals(participant.Establishment.WebsiteUrl, StringComparison.OrdinalIgnoreCase)
                            ||
                            // or the participant has ancestors whose id matches
                            participant.Establishment.Ancestors.Any
                            (
                                hierarchy =>
                                key.Equals(hierarchy.Ancestor.WebsiteUrl, StringComparison.OrdinalIgnoreCase)
                            )
                        )
                    );
            throw new NotSupportedException(string.Format(
                "Establishment key type '{0}' was not expected.", establishmentKey.GetType()));

            #endregion
        }

        internal static IQueryable<InstitutionalAgreement> MatchingPlaceParticipantOrContact(this IQueryable<InstitutionalAgreement> queryable, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return queryable;

            var matchesPlaceParticipantOrContact = NonOwnerPlaceNameMatches(keyword)
                .Or(ParticipantNameMatches(keyword))
                .Or(ContactMatches(keyword));

            return queryable.AsExpandable().Where(matchesPlaceParticipantOrContact);
        }

        private static Expression<Func<InstitutionalAgreement, bool>> NonOwnerPlaceNameMatches(string keyword)
        {
            var twoLetterIsoLanguageCode = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            #region where participant does not own the agreement, and the keyword matches a participant place name

            return
                agreement =>
                agreement.Participants.Any
                (
                    participant =>
                    !participant.IsOwner // participant does not own the agreement, and
                    &&
                    (
                        // either the participant has an official place name matching the keyword,
                        participant.Establishment.Location.Places.Any
                        (
                            place =>
                            place.OfficialName.Contains(keyword)
                        )
                        // or the participant has a translated place name matching the keyword in the user's language,
                        ||
                        participant.Establishment.Location.Places.Any
                        (
                            place =>
                            place.Names.Any
                            (
                                name =>
                                name.TranslationToLanguage != null
                                &&
                                name.TranslationToLanguage.TwoLetterIsoCode == twoLetterIsoLanguageCode
                                &&
                                name.Text.Contains(keyword)
                            )
                        )
                        // or the participant has a translated place name matching they keyword in the user's language, using only ASCII characters
                        ||
                        participant.Establishment.Location.Places.Any
                        (
                            place =>
                            place.Names.Any
                            (
                                name =>
                                name.TranslationToLanguage != null
                                &&
                                name.TranslationToLanguage.TwoLetterIsoCode == twoLetterIsoLanguageCode
                                &&
                                name.AsciiEquivalent != null
                                &&
                                name.AsciiEquivalent.Contains(keyword)
                            )
                        )
                    )
                );

            #endregion
        }

        private static Expression<Func<InstitutionalAgreement, bool>> ParticipantNameMatches(string keyword)
        {
            var twoLetterIsoLanguageCode = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            #region where participant name matches keyword

            return
                agreement =>
                agreement.Participants.Any
                (
                    participant =>
                    (
                        // either the participant has an official name matching the keyword,
                        participant.Establishment.OfficialName.Contains(keyword)
                        ||
                        // or the participant has a non-official name matching the keyword in the user's language,
                        participant.Establishment.Names.Any
                        (
                            name =>
                            name.TranslationToLanguage != null
                            &&
                            name.TranslationToLanguage.TwoLetterIsoCode == twoLetterIsoLanguageCode
                            &&
                            name.Text.Contains(keyword)
                        )
                        // or the participant has a non-official name matching the keyword in the user's language, using only ASCII characters
                        ||
                        participant.Establishment.Names.Any
                        (
                            name =>
                            name.TranslationToLanguage != null
                            &&
                            name.TranslationToLanguage.TwoLetterIsoCode == twoLetterIsoLanguageCode
                            &&
                            name.AsciiEquivalent != null
                            &&
                            name.AsciiEquivalent.Contains(keyword)
                        )
                    )
                );

            #endregion
        }

        private static Expression<Func<InstitutionalAgreement, bool>> ContactMatches(string keyword)
        {
            #region where contact matches keyword

            return agreement =>
                agreement.Contacts.Any
                (
                    contact =>
                        // contact display name matches the keyword,
                    contact.Person.DisplayName.Contains(keyword)
                    ||
                        // or contact first name matches the keyword,
                    (
                        contact.Person.FirstName != null
                        &&
                        contact.Person.FirstName.Contains(keyword)
                    )
                        // or contact last name matches the keyword,
                    ||
                    (
                        contact.Person.LastName != null
                        &&
                        contact.Person.LastName.Contains(keyword)
                    )
                );

            #endregion
        }
    }
}
