namespace UCosmic
{
    public interface ISignUsers
    {
        string DefaultSignedOnUrl { get; }
        void SignOn(string userName, bool remember = false, string scope = null);
    }
}
