namespace UCosmic.Domain.Files
{
    public class LooseFile : RevisableEntity
    {
        protected internal LooseFile()
        {
        }

        public byte[] Content { get; protected internal set; }
        public int Length { get; protected internal set; }
        public string MimeType { get; protected internal set; }
        public string Name { get; protected internal set; }
    }
}