using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc
{
    public static class ExtensionMethods
    {
        #region String formatting and templates

        //public static string FormatTemplate(this string template, IEnumerable<KeyValuePair<string, string>> replacements)
        //{
        //    if (string.IsNullOrWhiteSpace(template))
        //        return template;

        //    var content = new StringBuilder(template);
        //    if (replacements != null)
        //    {
        //        foreach (var replacement in replacements)
        //        {
        //            content.Replace(replacement.Key, replacement.Value);
        //        }
        //    }

        //    return content.ToString();
        //}

        public static MvcHtmlString LineBreaksToHtml(this MvcHtmlString template)
        {
            if (template == null)
                return null;

            var content = template.ToString().FormatTemplate(new Dictionary<string, string> { { "\r\n", "<br/>" } });
            return new MvcHtmlString(content);
        }

        public static int ParseIntoInt(this string stringValue, int defaultValue = default(int))
        {
            if (!string.IsNullOrWhiteSpace(stringValue))
            {
                int parsedInt;
                if (int.TryParse(stringValue, out parsedInt))
                {
                    return parsedInt;
                }
            }
            return defaultValue;
        }

        public static bool ParseIntoBool(this string stringValue, bool defaultValue = default(bool))
        {
            if (!string.IsNullOrWhiteSpace(stringValue))
            {
                bool parsedBool;
                if (bool.TryParse(stringValue, out parsedBool))
                {
                    return parsedBool;
                }
            }
            return defaultValue;
        }

        #endregion
        #region WasSignedInAs Session Shortcuts

        private const string WasSignedInAsKey = "WasSignedInAs";

        public static void WasSignedInAs(this HttpSessionStateBase session, string value)
        {
            if (session != null)
            {
                session[WasSignedInAsKey] = value;
            }
        }

        public static string WasSignedInAs(this HttpSessionStateBase session, bool keep = true)
        {
            if (session != null)
            {
                var value = session[WasSignedInAsKey];
                if (!keep)
                {
                    session.Remove(WasSignedInAsKey);
                }
                if (value != null)
                {
                    return value.ToString();
                }
            }
            return null;
        }

        #endregion
        #region FeedbackMessage TempData Shortcuts

        public static void FeedbackMessage(this TempDataDictionary tempData, string value)
        {
            if (tempData != null)
            {
                tempData[BaseController.FeedbackMessageKey] = value;
            }
        }

        public static string FeedbackMessage(this TempDataDictionary tempData, bool keep = false)
        {
            if (tempData != null)
            {
                var value = tempData[BaseController.FeedbackMessageKey];
                if (keep)
                {
                    tempData.Keep(BaseController.FeedbackMessageKey);
                }
                if (value != null)
                {
                    return value.ToString();
                }
            }
            return null;
        }

        #endregion
        #region EmailConfirmation TempData Shortcuts

        private const string EmailConfirmationTicketKey = "EmailConfirmationTicket";

        public static void EmailConfirmationTicket(this TempDataDictionary tempData, string ticket)
        {
            if (tempData == null) return;
            if (string.IsNullOrWhiteSpace(ticket))
                tempData.Remove(EmailConfirmationTicketKey);
            else tempData[EmailConfirmationTicketKey] = ticket;
        }

        public static string EmailConfirmationTicket(this TempDataDictionary tempData, bool keep = true)
        {
            if (tempData == null || !tempData.ContainsKey(EmailConfirmationTicketKey))
                return null;

            var value = tempData[EmailConfirmationTicketKey];
            if (keep) tempData.Keep(EmailConfirmationTicketKey);
            else tempData.Remove(SigningEmailAddressKey);

            return value != null ? value.ToString() : null;
        }

        #endregion
        #region SigningEmailAddress TempData Shortcuts

        private const string SigningEmailAddressKey = "SigningEmailAddress";

        public static void SigningEmailAddress(this TempDataDictionary tempData, string emailAddress)
        {
            if (tempData == null) return;
            if (string.IsNullOrWhiteSpace(emailAddress))
                tempData.Remove(SigningEmailAddressKey);
            else tempData[SigningEmailAddressKey] = emailAddress;
        }

        public static string SigningEmailAddress(this TempDataDictionary tempData, bool keep = true)
        {
            if (tempData == null || !tempData.ContainsKey(SigningEmailAddressKey))
                return null;

            var value = tempData[SigningEmailAddressKey];
            if (keep) tempData.Keep(SigningEmailAddressKey);
            else tempData.Remove(SigningEmailAddressKey);

            return value != null ? value.ToString() : null;
        }

        #endregion
        #region SigningEmailAddress Cookie Shortcuts

        public static void SigningEmailAddressCookie(this HttpContextBase httpContext, string emailAddress, bool rememberMe = false)
        {
            if (httpContext == null) return;
            HttpCookie cookie;

            // when email address is null or remember me is false, clear the cookie
            if (string.IsNullOrWhiteSpace(emailAddress) || !rememberMe)
                cookie = new HttpCookie(SigningEmailAddressKey, null)
                {
                    Expires = DateTime.UtcNow.AddDays(-1),
                };

            // otherwise when rememberMe is true, set the cookie
            else
                cookie = new HttpCookie(SigningEmailAddressKey, emailAddress)
                {
                    Expires = DateTime.UtcNow.AddDays(30),
                    Path = "/",
                };

            httpContext.Response.SetCookie(cookie);
        }

        public static string SigningEmailAddressCookie(this HttpContextBase httpContext, bool renew = true)
        {
            if (httpContext == null) return null;
            string value = null;

            // get cookie from request
            var cookie = httpContext.Request.Cookies[SigningEmailAddressKey];
            if (cookie != null) value = cookie.Value;

            if (!string.IsNullOrWhiteSpace(value) && renew)
                httpContext.Response.SetCookie(
                    new HttpCookie(SigningEmailAddressKey, value)
                    {
                        Expires = DateTime.UtcNow.AddDays(30),
                        Path = "/",
                    }
                );

            return value;
        }

        //public static bool HasSigningEmailAddressCookie(this HttpContextBase httpContext)
        //{
        //    if (httpContext == null) return false;
        //    var cookie = httpContext.Request.Cookies[SigningEmailAddressKey];
        //    return cookie != null && !string.IsNullOrWhiteSpace(cookie.Value);
        //}

        #endregion
        #region Skin Cookie Shortcuts

        private const string SkinKey = "skin";

        public static void SkinCookie(this HttpContextBase httpContext, string value)
        {
            if (httpContext == null) return;
            HttpCookie cookie;

            // when value is null or whitespace, clear the value
            if (string.IsNullOrWhiteSpace(value))
                cookie = new HttpCookie(SkinKey, null)
                {
                    Expires = DateTime.UtcNow.AddDays(-1),
                };

            // otherwise set the cookie
            else
                cookie = new HttpCookie(SkinKey, value)
                {
                    Expires = DateTime.UtcNow.AddDays(30),
                    Path = "/",
                };

            httpContext.Response.SetCookie(cookie);
        }

        public static string SkinCookie(this HttpContextBase httpContext, bool renew = true)
        {
            if (httpContext == null) return null;
            string value = null;

            // get cookie from request
            var cookie = httpContext.Request.Cookies[SkinKey];
            if (cookie != null) value = cookie.Value;

            if (!string.IsNullOrWhiteSpace(value) && renew)
                httpContext.Response.SetCookie(
                    new HttpCookie(SkinKey, value)
                    {
                        Expires = DateTime.UtcNow.AddDays(30),
                        Path = "/",
                    }
                );

            return value;
        }

        #endregion
        #region FailedPasswordAttempt Session Shortcuts

        private const string FailedPasswordAttemptsKey = "FailedPasswordAttempts";

        public static void FailedPasswordAttempt(this HttpSessionStateBase session)
        {
            if (session == null) return;

            session[FailedPasswordAttemptsKey] = session.FailedPasswordAttempts() + 1;
        }

        public static int FailedPasswordAttempts(this HttpSessionStateBase session, bool keep = true)
        {
            if (session != null)
            {
                var value = session[FailedPasswordAttemptsKey];
                if (!keep)
                {
                    session.Remove(FailedPasswordAttemptsKey);
                }
                if (value is int)
                {
                    return (int)value;
                }
            }
            return 0;
        }

        #endregion
        #region File names & paths

        public static string GetFileName(this string filePath)
        {
            return filePath.IndexOf("\\", StringComparison.Ordinal) >= 0
                ? filePath.Substring(filePath.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                : filePath;
        }

        public static bool HasFileName(this string fileName)
        {
            return fileName.IndexOf(".", StringComparison.Ordinal) > 0;
        }

        public static bool HasFileExtension(this string fileName)
        {
            return fileName.IndexOf(".", StringComparison.Ordinal) >= 0;
        }

        public static bool HasValidFileExtension(this string fileName, string allowedExtensions)
        {
            var indexOfExtension = fileName.LastIndexOf(".", StringComparison.Ordinal);
            if (indexOfExtension > 0)
            {
                var fileExtension = fileName.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal));
                var validExtensions = allowedExtensions.Split(',').Where(e => !string.IsNullOrWhiteSpace(e)).ToArray();
                foreach (var validExtension in validExtensions.Select(validExtension => validExtension.Trim()))
                {
                    var currentExtension = validExtension;
                    if (!currentExtension.StartsWith("."))
                        currentExtension = string.Format(".{0}", currentExtension);

                    if (fileExtension.Equals(currentExtension, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }
            return false;
        }

        //public static BinaryFile ToBinaryFile(this HttpPostedFileBase postedFile)
        //{
        //    if (postedFile == null) return null;

        //    var fileContent = new byte[postedFile.ContentLength];
        //    postedFile.InputStream.Read(fileContent, 0, postedFile.ContentLength);
        //    var fileName = postedFile.FileName;
        //    if (fileName.IndexOf('\\') > 0)
        //        fileName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
        //    return new BinaryFile
        //    {
        //        Content = fileContent,
        //        ContentLength = postedFile.ContentLength,
        //        ContentType = postedFile.ContentType,
        //        Name = fileName,
        //    };
        //}

        #endregion
    }
}