using System.Web;
using AutoMapper;
using UCosmic.Domain.Files;

namespace UCosmic.Www.Mvc.Models
{
    public static class HttpPostedFileBaseProfiler
    {
        public class HttpPostedFileBaseToCreateLooseFileCommandProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<HttpPostedFileBase, CreateLooseFileCommand>()
                    .ForMember(target => target.Content, opt => opt
                        .ResolveUsing(source =>
                        {
                            var contentLength = source.ContentLength;
                            var content = new byte[contentLength];
                            source.InputStream.Read(content, 0, contentLength);
                            return content;
                        }))
                    .ForMember(target => target.MimeType, opt => opt
                        .ResolveUsing(source => source.ContentType))
                    .ForMember(target => target.Name, opt => opt
                        .ResolveUsing(source => source.FileName.GetFileName()))
                    .ForMember(target => target.CreatedLooseFile, opt => opt.Ignore())
                ;
            }
        }
    }
}