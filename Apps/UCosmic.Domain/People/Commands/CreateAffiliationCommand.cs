namespace UCosmic.Domain.People
{
    public class CreateAffiliationCommand
    {
        public int PersonId { get; set; }
        public int EstablishmentId { get; set; }
        public bool IsClaimingStudent { get; set; }
        public bool IsClaimingEmployee { get; set; }
    }
}
