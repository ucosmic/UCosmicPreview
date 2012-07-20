namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreementFile : RevisableEntity
    {
        public virtual InstitutionalAgreement Agreement { get; set; }

        public byte[] Content { get; set; }
        public int Length { get; set; }
        public string MimeType { get; set; }
        public string Name { get; set; }

        //internal int Remove(ICommandObjects commander)
        //{
        //    commander.Delete(this);
        //    return 1;
        //}

        internal int Remove(ICommandEntities entities)
        {
            entities.Purge(this);
            return 1;
        }
    }
}