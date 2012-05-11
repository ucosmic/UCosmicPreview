namespace UCosmic
{
    public interface IStorePasswords
    {
        int MaximumPasswordAttempts { get; }
        int MinimumPasswordLength { get; }
        bool Exists(string userName);
        bool IsLockedOut(string userName);
        bool Validate(string userName, string password);
        void Create(string userName, string password);
        void Destroy(string userName);
        void Reset(string userName, string password);
        void Update(string userName, string oldPassword, string newPassword);
    }
}