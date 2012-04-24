namespace UCosmic
{
    public interface IProcessQueries
    {
        TResult Execute<TResult>(IDefineQuery<TResult> query);
    }
}
