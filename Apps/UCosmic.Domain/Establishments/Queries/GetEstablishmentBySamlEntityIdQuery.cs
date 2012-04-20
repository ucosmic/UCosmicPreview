
namespace UCosmic.Domain.Establishments
{
    public class GetEstablishmentBySamlEntityIdQuery : BaseEstablishmentQuery, IDefineQuery<Establishment>
    {
        public string SamlEntityId { get; set; }
    }
}
