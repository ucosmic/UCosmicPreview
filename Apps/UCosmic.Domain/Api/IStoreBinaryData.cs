namespace UCosmic
{
    public interface IStoreBinaryData
    {
        bool Exists(string path);
        void Put(string path, byte[] data, bool overwrite = false);
        byte[] Get(string path);
        void Delete(string path);
    }
}