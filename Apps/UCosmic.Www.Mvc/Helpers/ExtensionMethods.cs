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

        // ReSharper disable UnusedMember.Global
        public static bool ParseIntoBool(this string stringValue, bool defaultValue = default(bool))
        // ReSharper restore UnusedMember.Global
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
            if (tempData == null) return null;

            var value = tempData[EmailConfirmationTicketKey];
            if (keep) tempData.Keep(EmailConfirmationTicketKey);

            return value != null ? value.ToString() : null;
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
            var indexOfExtension = fileName.LastIndexOf(".");
            if (indexOfExtension > 0)
            {
                var fileExtension = fileName.Substring(fileName.LastIndexOf("."));
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
        #region Routing multiple URL's

        public static void MapRoutes(this AreaRegistrationContext context,
            string name, IEnumerable<string> urls, object defaults, object constraints = null)
        {
            foreach (var url in urls) context.MapRoute(name, url, defaults, constraints);
        }

        #endregion
    }
}