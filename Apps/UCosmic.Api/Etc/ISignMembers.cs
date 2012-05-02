namespace UCosmic
{
    public interface ISignMembers
    {
        int MaximumPasswordAttempts { get; }
        int MinimumPasswordLength { get; }
        bool IsSignedUp(string userName);
        bool IsLockedOut(string userName);
        bool Validate(string userName, string password);
        void SignUp(string userName, string password);
        void ResetPassword(string userName, string password);
    }
}