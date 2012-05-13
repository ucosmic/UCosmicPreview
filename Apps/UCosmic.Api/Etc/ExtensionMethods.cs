using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace UCosmic
{
    public static class ExtensionMethods
    {
        #region Security

        public static IPrincipal AsPrincipal(this string principalIdentityName)
        {
            if (principalIdentityName == null) return null;
            var principal = new GenericPrincipal(new GenericIdentity(principalIdentityName), null);
            return principal;
        }

        #endregion
        #region Url Encoding & Decoding

        public static string UrlEncoded(this string value)
        {
            return (value != null) ? HttpUtility.UrlEncode(value) : null;
        }

        public static string UrlPathEncoded(this string value)
        {
            return (value != null) ? HttpUtility.UrlPathEncode(value) : null;
        }

        public static string UrlDecoded(this string value)
        {
            return (value != null) ? HttpUtility.UrlDecode(value) : null;
        }

        #endregion
        #region Email Addresses

        public static string GetEmailDomain(this string email)
        {
            if (email == null) throw new ArgumentNullException("email");

            const char at = '@';
            if (!email.Contains(at))
                throw new InvalidOperationException(
                    "The string '{0}' is missing its '{1}' character.".FormatWith(email, at));
            if (email.IndexOf(at) != email.LastIndexOf(at))
                throw new InvalidOperationException(
                    "The string '{0}' has more than one '{1}' characters.".FormatWith(email, at));

            return email.Substring(email.LastIndexOf(at));
        }

        #endregion
        #region String to enum

        private static readonly IDictionary<Type, IDictionary<string, object>> StringsToEnums = new Dictionary<Type, IDictionary<string, object>>();

        public static TEnum AsEnum<TEnum>(this string stringValue) where TEnum : struct, IConvertible
        {
            // check invoked argument and constrain generic argument
            var typeOfEnum = typeof(TEnum);
            if (stringValue.IsNullOrWhiteSpace())
                throw new ArgumentException("Cannot be null or whitespace.", "stringValue");
            if (!typeOfEnum.IsEnum) throw new ArgumentException("TEnum must be an enumerated type.");

            // initialize the dictionary
            if (!StringsToEnums.ContainsKey(typeOfEnum))
                StringsToEnums.Add(typeOfEnum, new Dictionary<string, object>());

            // defragment the string
            stringValue = stringValue.Replace(" ", string.Empty);

            // check the dictionary
            var pair = StringsToEnums[typeOfEnum];
            if (pair.ContainsKey(stringValue))
                return (TEnum)pair[stringValue];

            // parse
            var parsed = Enum.Parse(typeof(TEnum), stringValue);
            pair.Add(stringValue, parsed);
            return (TEnum)pair[stringValue];
        }

        #endregion
        #region Enum to string

        private static readonly IDictionary<Type, IDictionary<object, string>> EnumsToStrings = new Dictionary<Type, IDictionary<object, string>>();

        public static string AsSentenceFragment<TEnum>(this TEnum enumValue) where TEnum : struct, IConvertible
        {
            // check invoked argument and constrain generic argument
            var typeOfEnum = typeof(TEnum);
            if (!typeOfEnum.IsEnum) throw new ArgumentException("TEnum must be an enumerated type.");

            // initialize the dictionary
            if (!EnumsToStrings.ContainsKey(typeOfEnum))
                EnumsToStrings.Add(typeOfEnum, new Dictionary<object, string>());

            // check the dictionary
            var pair = EnumsToStrings[typeOfEnum];
            if (pair.ContainsKey(enumValue))
                return pair[enumValue];

            // fragment
            var fragmented = new StringBuilder();
            var characters = enumValue.ToInvariantString().ToCharArray();
            foreach (var character in characters)
                if (character.ToInvariantString() == character.ToInvariantString().ToUpper())
                    fragmented.Append(" {0}".FormatWith(character));
                else
                    fragmented.Append(character);

            // parse
            var sentence = fragmented.ToString().Trim();
            pair.Add(enumValue, sentence);
            return pair[enumValue];
        }

        #endregion
        #region String formatting

        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static string FormatTemplate(this string template, IEnumerable<KeyValuePair<string, string>> replacements)
        {
            if (template.IsNullOrWhiteSpace())
                return template;

            var content = new StringBuilder(template);
            if (replacements != null)
            {
                foreach (var replacement in replacements)
                {
                    content.Replace(replacement.Key, replacement.Value);
                }
            }

            return content.ToString();
        }

        #endregion
        #region Null check shortcuts

        public static bool IsNull(this object value)
        {
            return value == null;
        }

        public static bool IsNotNull(this object value)
        {
            return !value.IsNull();
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotNullOrWhiteSpace(this string value)
        {
            return !value.IsNullOrWhiteSpace();
        }

        #endregion
        #region Semicolon Explosion

        public static IEnumerable<string> Explode(this string imploded, string delimiter)
        {
            if (imploded != null)
            {
                var exploded = new List<string>();
                if (imploded.Contains(delimiter))
                {
                    var split = imploded.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
                    exploded.AddRange(split);
                }
                else
                {
                    exploded.Add(imploded);
                }
                return exploded;
            }
            return null;
        }

        #endregion
        #region Globalization Shortcuts

        public static string ToInvariantString(this object value)
        {
            if (value == null) throw new ArgumentNullException("value");
            return value.ToString();
        }

        #endregion
        #region Hexidecimal Conversion

        public static byte[] ToHexBytes(this string hex)
        {
            if (hex.IsNullOrWhiteSpace()) return null;

            var charCount = hex.Length;
            var bytes = new byte[charCount / 2];
            for (var i = 0; i < charCount; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        // ReSharper disable UnusedMember.Global

        public static string ToHexString(this byte[] bytes)
        // ReSharper restore UnusedMember.Global
        {
            if (bytes == null || bytes.Length < 1) return null;

            return BitConverter.ToString(bytes).Replace("-", String.Empty);
        }

        #endregion
        #region Attributes

        public static TAttribute[] GetAttributes<TTarget, TType, TAttribute>(this Expression<Func<TTarget, TType>> expression,
            bool inherit = false)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression != null)
                return memberExpression.Member.GetCustomAttributes(typeof(TAttribute), inherit) as TAttribute[];

            var methodCallExpression = expression.Body as MethodCallExpression;
            if (methodCallExpression != null)
                return methodCallExpression.Method.GetCustomAttributes(typeof(TAttribute), inherit) as TAttribute[];

            throw new NotSupportedException("GetAttributes expression body was unexpected.");
        }

        #endregion
    }
}
