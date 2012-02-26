using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web;

namespace UCosmic
{
    public static class ExtensionMethods
    {
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
        #region Template Formatting

        public static string FormatTemplate(this string template, IEnumerable<KeyValuePair<string, string>> replacements)
        {
            if (String.IsNullOrWhiteSpace(template))
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
            if (value == null) throw new NullReferenceException();
            return value.ToString();
        }

        #endregion
        #region Hexidecimal Conversion

        public static byte[] ToHexBytes(this string hex)
        {
            if (string.IsNullOrWhiteSpace(hex)) return null;

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
