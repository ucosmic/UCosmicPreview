
namespace UCosmic.Domain.Establishments
{
    public class GetEstablishmentByIdQuery : IDefineQuery<Establishment>
    {
        public int Id { get; set; }
    }
}
