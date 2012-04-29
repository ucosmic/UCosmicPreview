using System;
using System.Linq;
using System.Reflection;
using System.Web;
using AutoMapper;
using UCosmic.Domain.Files;

namespace UCosmic.Www.Mvc.Models
{
    public static class RootModelProfiler
    {
        public static void RegisterProfiles()
        {
            RegisterProfiles(typeof(RootModelProfiler));
        }

        public static void RegisterProfiles(Type modelMapper)
        {
            var nestedProfiles = modelMapper.GetNestedTypes(BindingFlags.NonPublic | BindingFlags.CreateInstance);
            var profiles = nestedProfiles.Select(Activator.CreateInstance).OfType<Profile>().ToList();
            AutoMapperRegistration.AddProfiles(profiles.ToArray());
        }

        // ReSharper disable UnusedMember.Local

        private class LooseFileFromHttpPostedFileBaseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<HttpPostedFileBase, LooseFile>()
                    .ForMember(target => target.Content, opt => opt
                        .ResolveUsing(source =>
                        {
                            var contentLength = source.ContentLength;
                            var content = new byte[contentLength];
                            source.InputStream.Read(content, 0, contentLength);
                            return content;
                        }))
                    .ForMember(target => target.Length, opt => opt
                        .ResolveUsing(source => source.ContentLength))
                    .ForMember(target => target.MimeType, opt => opt
                        .ResolveUsing(source => source.ContentType))
                    .ForMember(target => target.Name, opt => opt
                        .ResolveUsing(source => source.FileName.GetFileName()))
                    .ForMember(target => target.EntityId, opt => opt.Ignore())
                    .ForMember(target => target.CreatedByPrincipal, opt => opt.Ignore())
                    .ForMember(target => target.CreatedOnUtc, opt => opt.Ignore())
                    .ForMember(target => target.UpdatedByPrincipal, opt => opt.Ignore())
                    .ForMember(target => target.UpdatedOnUtc, opt => opt.Ignore())
                    .ForMember(target => target.IsCurrent, opt => opt.Ignore())
                    .ForMember(target => target.IsArchived, opt => opt.Ignore())
                    .ForMember(target => target.IsDeleted, opt => opt.Ignore())
                    .ForMember(target => target.RevisionId, opt => opt.Ignore())
                    .ForMember(target => target.Version, opt => opt.Ignore())
                ;
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}