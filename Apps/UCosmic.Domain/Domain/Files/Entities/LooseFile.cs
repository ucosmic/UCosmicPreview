namespace UCosmic.Domain.Files
{
    public class LooseFile : RevisableEntity
    {
        public byte[] Content { get; internal set; }
        public int Length { get; internal set; }
        public string MimeType { get; internal set; }
        public string Name { get; internal set; }
    }
}