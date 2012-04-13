namespace UCosmic.Domain.Establishments
{
    public class UpdateSamlSignOnInfoCommand
    {
        public Establishment Establishment { get; set; }
        public string EntityId { get; set; }
        public string MetadataUrl { get; set; }
    }
}
