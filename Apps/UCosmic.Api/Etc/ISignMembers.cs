namespace UCosmic
{
    public interface ISignMembers
    {
        bool IsSignedUp(string userName);
        void SignUp(string userName, string password);
        void SignIn(string userName);
        string DefaultSignedInUrl { get; }
    }
}