namespace UCosmic
{
    public interface ISignMembers
    {
        bool IsSignedUp(string userName);
        void SignUp(string userName, string password);
        void ResetPassword(string userName, string password);
    }
}