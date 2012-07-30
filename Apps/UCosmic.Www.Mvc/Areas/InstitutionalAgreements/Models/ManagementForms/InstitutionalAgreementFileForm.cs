using System;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Www.Mvc.Models;
using UCosmic.Domain.Files;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementFileForm
    {
        private const string AllowedExtensions = "PDF, DOC, DOCX, ODT, XLS, XLSX, ODS, PPT, PPTX";
        public const string InvalidExtensionErrorText =
            "You may only upload PDF, Microsoft Office, and Open Document files with a pdf, doc, docx, odt, xls, xlsx, ods, ppt, or pptx extension.";

        [HiddenInput(DisplayValue = false)]
        public bool IsDeleted { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int RevisionId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid EntityId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Name { get; set; }

        [FileExtensionRequired(ErrorMessage = "You cannot upload {0} because it does not have an extension.")]
        [FileNameRequired(ErrorMessage = "You cannot upload {0} because the file name begins with a dot (.) character.")]
        [AllowedFileExtensions(ErrorMessage = "You cannot upload {0} because it has a {1} extension.",
            AllowedExtensions = AllowedExtensions)]
        public HttpPostedFileBase PostedFile { get; set; }

        // TODO: should this be a validation attribute?
        public bool IsValidPostedFile
        {
            get
            {
                if (PostedFile != null)
                {
                    var fileName = PostedFile.FileName.GetFileName();
                    return fileName.HasFileName() && fileName.HasFileExtension() &&
                           fileName.HasValidFileExtension(AllowedExtensions);
                }
                return true;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, IsDeleted = {1}", Name, IsDeleted);
        }
    }

    public static class InstitutionalAgreementFileFormProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(InstitutionalAgreementFileFormProfiler));
        }

        internal class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreementFile, InstitutionalAgreementFileForm>()
                    .ForMember(m => m.PostedFile, o => o.Ignore())
                ;

                CreateMap<LooseFile, InstitutionalAgreementFileForm>()
                    .ForMember(target => target.PostedFile, o => o.UseValue(null))
                ;
            }
        }
    }
}