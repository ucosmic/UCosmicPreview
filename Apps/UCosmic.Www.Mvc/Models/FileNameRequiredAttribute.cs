using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace UCosmic.Www.Mvc.Models
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileNameRequiredAttribute : ValidationAttribute
    {
        private string _fileName;

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, _fileName);
        }

        public override bool IsValid(object value)
        {
            var httpPostedFile = value as HttpPostedFileBase;
            if (httpPostedFile != null)
            {
                _fileName = httpPostedFile.FileName.GetFileName();
                if (!_fileName.HasFileName())
                    return false;

            }
            return true;
        }
    }

}