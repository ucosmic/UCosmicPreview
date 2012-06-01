using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FluentValidation;
using UCosmic.Domain.Activities;
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

        public ActivityMode Mode { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Number { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Prompt = "[Enter tags here]")]
        public string TagSearch { get; set; }

        [HiddenInput(DisplayValue = false)]
        [RangeIfClient(1, int.MaxValue, "Mode", ComparisonType.IsNotEqualTo, ActivityMode.Draft, ErrorMessage = FormValidator.FailedBecauseTagsWasEmpty)]
        public int TagCount { get; set; }

        public Tag[] Tags { get; set; }
        public class Tag
        {
            public int? RevisionId { get; set; }
            public string TaggedText { get; set; }
            public ActivityTagDomainType DomainType { get; set; }
            public bool IsDeleted { get; set; }
        }

        [DataType(DataType.Date)]
        [Display(Prompt = "[Start Date]")]
        [RequiredIfClient("Mode", ComparisonType.IsNotEqualTo, ActivityMode.Draft, ErrorMessage = FormValidator.FailedBecauseStartsOnWasEmpty)]
        public DateTime? StartsOn { get; set; }

        [DataType(DataType.Date)]
        [Display(Prompt = "[End Date]")]
        public DateTime? EndsOn { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }

    public class FormValidator : AbstractValidator<Form>
    {
        public const string FailedBecauseTitleWasEmpty = "Title is required.";
        public const string FailedBecauseContentWasEmpty = "Description is required.";
        public const string FailedBecauseTagsWasEmpty = "At least 1 tag is required.";
        public const string FailedBecauseStartsOnWasEmpty = "Start date is required.";

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
                RuleFor(m => m.TagCount)
                    .Must((o, p) => o.Tags != null && o.Tags.Any(t => !t.IsDeleted))
                    .WithMessage(FailedBecauseTagsWasEmpty);
                RuleFor(o => o.StartsOn)
                    .NotEmpty()
                    .WithMessage(FailedBecauseStartsOnWasEmpty);
            });
        }
    }

    public static class FormProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(FormProfiler));
        }

        private class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Activity, Form>()
                    .ForMember(d => d.Title, o => o.MapFrom(s => s.DraftedValues.Title))
                    .ForMember(d => d.Content, o => o.MapFrom(s => s.DraftedValues.Content))
                    .ForMember(d => d.StartsOn, o => o.MapFrom(s => s.DraftedValues.StartsOn))
                    .ForMember(d => d.EndsOn, o => o.MapFrom(s => s.DraftedValues.EndsOn))
                    .ForMember(d => d.Tags, o => o.MapFrom(s => Mapper.Map<Form.Tag[]>(s.DraftedTags)))
                    .ForMember(d => d.TagSearch, o => o.Ignore())
                    .ForMember(d => d.TagCount, o => o.Ignore())
                    .ForMember(d => d.ReturnUrl, o => o.Ignore())
                ;

                CreateMap<DraftedTag, Form.Tag>()
                    .ForMember(d => d.TaggedText, o => o.MapFrom(s => s.Text))
                    .ForMember(d => d.RevisionId, o => o.MapFrom(s => s.DomainKey))
                    .ForMember(d => d.IsDeleted, o => o.Ignore())
                ;
            }
        }
    }
}