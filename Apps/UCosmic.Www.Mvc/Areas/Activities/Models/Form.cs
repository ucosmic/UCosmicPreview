using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Activities.Models
{
    public class Form : IReturnUrl
    {
        [DataType(DataType.MultilineText)]
        [Display(Prompt = "[Enter the title or main heading of your activity here]")]
        public string Title { get; set; }
        public const string TitlePropertyName = "Title";

        [Remote("ValidateTitle", "Form", "Activities", HttpMethod = "POST", AdditionalFields = "Title,SelectedMode")]
        public string TitleValidator { get { return TitlePropertyName; } }

        [AllowHtml]
        [UIHint("TinyMceContent")]
        public string Content { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Prompt = "[Enter tags here]")]
        public string TagSearch { get; set; }

        public ActivityMode Mode { get; set; }

        [HiddenInput(DisplayValue = false)]
        public ActivityMode SelectedMode { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Number { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

        public Tag[] Tags { get; set; }
        public class Tag
        {
            public int? RevisionId { get; set; }
            public string TaggedText { get; set; }
            public TagDomainType DomainType { get; set; }
            public bool IsDeleted { get; set; }
        }
    }

    public class FormValidator : AbstractValidator<Form>
    {
        public FormValidator()
        {
            When(m => m.SelectedMode != ActivityMode.Draft, () =>
            {
                RuleFor(m => m.Title)
                    .NotEmpty()
                    .WithMessage("Title is required.");
                RuleFor(m => m.Content)
                    .NotEmpty()
                    .WithMessage("Description is required.");
            });
        }
    }
}