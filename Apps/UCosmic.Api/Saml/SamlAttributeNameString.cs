using System;

namespace UCosmic
{
    public static class SamlAttributeNameString
    {
        private const string DisplayName = "urn:oid:2.16.840.1.113730.3.1.241";

        public static SamlAttributeName AsSamlAttributeName(this string nameString)
        {
            switch (nameString)
            {
                case DisplayName:
                    return SamlAttributeName.DisplayName;
            }
            throw new InvalidOperationException(string.Format(
                "SAML attribute name '{0}' was unexpected.", nameString));
        }

        public static string AsString(this SamlAttributeName nameEnum)
        {
            switch (nameEnum)
            {
                case SamlAttributeName.DisplayName:
                    return DisplayName;

                default:
                    return null;
            }
        }
    }
}
