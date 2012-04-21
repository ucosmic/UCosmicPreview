
namespace UCosmic.Domain.Establishments
{
    public class GetEstablishmentByIdQuery : BaseEstablishmentQuery, IDefineQuery<Establishment>
    {
        public int Id { get; set; }
    }
}
