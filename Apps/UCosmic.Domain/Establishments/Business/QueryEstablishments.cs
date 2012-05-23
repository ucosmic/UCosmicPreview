using System;
using System.Globalization;
using System.Linq;

namespace UCosmic.Domain.Establishments
{
    internal static class QueryEstablishments
    {
        internal static Establishment ByEmail(this IQueryable<Establishment> queryable, string email)
        {
            var emailDomain = email.GetEmailDomain();
            var establishment = queryable.SingleOrDefault
            (
                e =>
                e.EmailDomains.Any
                (
                    d =>
                    d.Value.Equals(emailDomain, StringComparison.OrdinalIgnoreCase)
                )
            );
            return establishment;
        }

        internal static Establishment BySamlEntityId(this IQueryable<Establishment> queryable, string entityId)
        {
            if (entityId == null) throw new ArgumentNullException("entityId");
            if (string.IsNullOrWhiteSpace(entityId)) throw new ArgumentException(string.Format(
                "The SAML EntityID '{0}' cannot be a null or whitespace string.", entityId));

            var establishment = queryable.SamlIntegrated().SingleOrDefault(e =>
                entityId.Equals(e.SamlSignOn.EntityId, StringComparison.OrdinalIgnoreCase));
            return establishment;
        }

        internal static IQueryable<Establishment> SamlIntegrated(this IQueryable<Establishment> queryable)
        {
            return queryable.Where(establishment => establishment.SamlSignOn != null);
        }

        internal static IQueryable<Establishment> IsRoot(this IQueryable<Establishment> queryable)
        {
            return queryable.Where(establishment => establishment.Parent == null);
        }

        internal static IQueryable<Establishment> IsNotRoot(this IQueryable<Establishment> queryable)
        {
            return queryable.Where(establishment => establishment.Parent != null);
        }

        internal static IQueryable<Establishment> WithAnyChildren(this IQueryable<Establishment> queryable)
        {
            return queryable.Where(establishment => establishment.Children.Any());
        }

        internal static IQueryable<Establishment> WithoutAnyChildren(this IQueryable<Establishment> queryable)
        {
            return queryable.Where(establishment => !establishment.Children.Any());
        }

        internal static IQueryable<Establishment> WithName(this IQueryable<Establishment> queryable, string term, StringMatchStrategy matchStrategy)
        {
            var currentLanguage = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            switch (matchStrategy)
            {
                case StringMatchStrategy.Equals:
                    return queryable.Where(
                        establishment =>
                        establishment.Names.Any
                        (
                            name =>
                            (
                                name.IsOfficialName
                                &&
                                (
                                    name.Text.Equals(term, StringComparison.OrdinalIgnoreCase)
                                    ||
                                    (
                                        name.AsciiEquivalent != null
                                        &&
                                        name.AsciiEquivalent.Equals(term, StringComparison.OrdinalIgnoreCase)
                                    )
                                )
                            )
                            ||
                            (
                                !name.IsOfficialName
                                &&
                                name.TranslationToLanguage != null
                                &&
                                name.TranslationToLanguage.TwoLetterIsoCode == currentLanguage
                                &&
                                (
                                    name.Text.Equals(term)
                                    ||
                                    (
                                        name.AsciiEquivalent != null
                                        &&
                                        name.AsciiEquivalent.Equals(term)
                                    )
                                )
                            )
                        )
                    );
                case StringMatchStrategy.StartsWith:
                    return queryable.Where(
                        establishment =>
                        establishment.Names.Any
                        (
                            name =>
                            (
                                name.IsOfficialName
                                &&
                                (
                                    name.Text.StartsWith(term)
                                    ||
                                    (
                                        name.AsciiEquivalent != null
                                        &&
                                        name.AsciiEquivalent.StartsWith(term)
                                    )
                                )
                            )
                            ||
                            (
                                !name.IsOfficialName
                                &&
                                name.TranslationToLanguage != null
                                &&
                                name.TranslationToLanguage.TwoLetterIsoCode == currentLanguage
                                &&
                                (
                                    name.Text.StartsWith(term)
                                    ||
                                    (
                                        name.AsciiEquivalent != null
                                        &&
                                        name.AsciiEquivalent.StartsWith(term)
                                    )
                                )
                            )
                        )
                    );
                case StringMatchStrategy.Contains:
                    return queryable.Where(
                        establishment =>
                        establishment.Names.Any
                        (
                            name =>
                            (
                                name.IsOfficialName
                                &&
                                (
                                    name.Text.Contains(term)
                                    ||
                                    (
                                        name.AsciiEquivalent != null
                                        &&
                                        name.AsciiEquivalent.Contains(term)
                                    )
                                )
                            )
                            ||
                            (
                                !name.IsOfficialName
                                &&
                                name.TranslationToLanguage != null
                                &&
                                name.TranslationToLanguage.TwoLetterIsoCode == currentLanguage
                                &&
                                (
                                    name.Text.Contains(term)
                                    ||
                                    (
                                        name.AsciiEquivalent != null
                                        &&
                                        name.AsciiEquivalent.Contains(term)
                                    )
                                )
                            )
                        )
                    );
            }
            throw new NotSupportedException(string.Format("StringMatchStrategy '{0}' is not supported.", matchStrategy));
        }
    }
}
