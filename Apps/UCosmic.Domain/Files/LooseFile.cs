namespace UCosmic.Domain.Files
{
    public class LooseFile : RevisableEntity
    {
        public byte[] Content { get; set; }
        public int Length { get; set; }
        public string MimeType { get; set; }
        public string Name { get; set; }
    }
}