namespace UCosmic
{
    public interface IHandleQueries<in TQuery, out TResult> where TQuery : IDefineQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}