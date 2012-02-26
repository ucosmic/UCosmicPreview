using System;

namespace UCosmic
{
    public static class SamlAttributeFriendlyNameString
    {
        private const string EduPersonPrincipalName = "eduPersonPrincipalName";

        public static SamlAttributeFriendlyName AsSamlAttributeFriendlyName(this string friendlyNameString)
        {
            switch (friendlyNameString)
            {
                case EduPersonPrincipalName:
                    return SamlAttributeFriendlyName.EduPersonPrincipalName;
            }
            throw new InvalidOperationException(string.Format(
                "SAML attribute friendly name '{0}' was unexpected.", friendlyNameString));
        }

        public static string AsString(this SamlAttributeFriendlyName friendlyNameEnum)
        {
            switch (friendlyNameEnum)
            {
                case SamlAttributeFriendlyName.EduPersonPrincipalName:
                    return EduPersonPrincipalName;
                default:
                    return null;
            }
        }
    }
}
