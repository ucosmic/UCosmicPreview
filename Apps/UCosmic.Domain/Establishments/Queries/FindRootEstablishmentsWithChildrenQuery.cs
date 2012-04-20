using System.Collections.Generic;

namespace UCosmic.Domain.Establishments
{
    public class FindRootEstablishmentsWithChildrenQuery : BaseEstablishmentQuery, IDefineQuery<ICollection<Establishment>>
    {
    }
}
