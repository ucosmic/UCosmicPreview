using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace UCosmic.Www.Mvc.Models
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AllowedFileExtensionsAttribute : ValidationAttribute
    {
        private string _fileName;
        private string _fileExtension;

        public string AllowedExtensions { get; set; }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, _fileName, _fileExtension);
        }

        public override bool IsValid(object value)
        {
            var httpPostedFile = value as HttpPostedFileBase;
            if (httpPostedFile != null)
            {
                if (httpPostedFile.ContentLength < 1) return true;

                _fileName = httpPostedFile.FileName.GetFileName();
                if (_fileName.LastIndexOf(".", StringComparison.Ordinal) < 0) return true;
                _fileExtension = httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf(".", StringComparison.Ordinal));
                return _fileName.HasValidFileExtension(AllowedExtensions);

            }
            return true;
        }
    }

}