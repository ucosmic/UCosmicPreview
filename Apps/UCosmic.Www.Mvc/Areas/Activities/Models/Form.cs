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
        [RequiredIfClient("Mode", ComparisonType.IsNotEqualTo, ActivityMode.Draft, ErrorMessage = FormValidator.FailedBecauseTitleWasEmpty)]
        public string Title { get; set; }
        public const string TitlePropertyName = "Title";

        [AllowHtml]
        [UIHint("TinyMceContent")]
        [RequiredIfClient("Mode", ComparisonType.IsNotEqualTo, ActivityMode.Draft, ErrorMessage = FormValidator.FailedBecauseContentWasEmpty)]
        public string Content { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Prompt = "[Enter tags here]")]
        public string TagSearch { get; set; }

        public ActivityMode Mode { get; set; }

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
        public const string FailedBecauseTitleWasEmpty = "Title is required.";
        public const string FailedBecauseContentWasEmpty = "Description is required.";

        public FormValidator()
        {
            When(m => m.Mode != ActivityMode.Draft, () =>
            {
                RuleFor(m => m.Title)
                    .NotEmpty()
                    .WithMessage(FailedBecauseTitleWasEmpty);
                RuleFor(m => m.Content)
                    .NotEmpty()
                    .WithMessage(FailedBecauseContentWasEmpty);
            });
        }
    }
}