using System;

namespace UCosmic
{
    public static class SamlAttributeFriendlyNameString
    {
        private const string EduPersonTargetedId = "eduPersonTargetedID";
        private const string EduPersonPrincipalName = "eduPersonPrincipalName";
        private const string EduPersonScopedAffiliation = "eduPersonScopedAffiliation";
        private const string CommonName = "cn";
        private const string SurName = "sn";
        private const string GivenName = "givenName";
        private const string Mail = "mail";

        public static SamlAttributeFriendlyName AsSamlAttributeFriendlyName(this string friendlyNameString)
        {
            switch (friendlyNameString)
            {
                case EduPersonTargetedId:
                    return SamlAttributeFriendlyName.EduPersonTargetedId;

                case EduPersonPrincipalName:
                    return SamlAttributeFriendlyName.EduPersonPrincipalName;

                case EduPersonScopedAffiliation:
                    return SamlAttributeFriendlyName.EduPersonScopedAffiliation;

                case CommonName:
                    return SamlAttributeFriendlyName.CommonName;

                case SurName:
                    return SamlAttributeFriendlyName.SurName;

                case GivenName:
                    return SamlAttributeFriendlyName.GivenName;

                case Mail:
                    return SamlAttributeFriendlyName.Mail;
            }
            throw new InvalidOperationException(string.Format(
                "SAML attribute friendly name '{0}' was unexpected.", friendlyNameString));
        }

        public static string AsString(this SamlAttributeFriendlyName friendlyNameEnum)
        {
            switch (friendlyNameEnum)
            {
                case SamlAttributeFriendlyName.EduPersonTargetedId:
                    return EduPersonTargetedId;

                case SamlAttributeFriendlyName.EduPersonPrincipalName:
                    return EduPersonPrincipalName;

                case SamlAttributeFriendlyName.EduPersonScopedAffiliation:
                    return EduPersonScopedAffiliation;

                case SamlAttributeFriendlyName.CommonName:
                    return CommonName;

                case SamlAttributeFriendlyName.SurName:
                    return SurName;

                case SamlAttributeFriendlyName.GivenName:
                    return GivenName;

                case SamlAttributeFriendlyName.Mail:
                    return Mail;

                default:
                    return null;
            }
        }
    }
}
