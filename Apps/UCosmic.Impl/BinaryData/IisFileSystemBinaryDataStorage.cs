using System;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using UCosmic.BinaryData;

namespace UCosmic.Impl.BinaryData
{
    // stores binary data as files on the local filesystem
    public class IisFileSystemBinaryDataStorage : IStoreBinaryData
    {
        private readonly string _root;

        public IisFileSystemBinaryDataStorage(string appRelativeRoot)
        {
            appRelativeRoot = appRelativeRoot ?? "~/";
            if (!appRelativeRoot.StartsWith("~/"))
                throw new ArgumentException(string.Format(
                    "The path '{0}' is not a valid app-relative root directory. App-relative directories begin with '~/'.",
                        appRelativeRoot));
            _root = HostingEnvironment.MapPath(appRelativeRoot);
        }

        private string GetFullPath(string relativePath)
        {
            // combine root with relative path for System.IO usage
            while (relativePath.StartsWith("/"))
                relativePath = relativePath.Substring(1);
            var fullPath = Path.Combine(_root, relativePath);
            return fullPath;
        }

        public bool Exists(string path)
        {
            path.ValidateAsBinaryStoragePath();

            // this does not work for directories, only files
            var fullPath = GetFullPath(path);
            var exists = File.Exists(fullPath);
            return exists;
        }

        public void Put(string path, byte[] data, bool overwrite = false)
        {
            path.ValidateAsBinaryStoragePath();

            // disallow file replacement unless specified in method invocation
            if (!overwrite && Exists(path))
                throw new InvalidOperationException(string.Format(
                    "A file already exists at the path '{0}'. To overwrite this file, invoke this method with overwrite == true.", path));

            // create directories if they do not already exist
            var fullPath = GetFullPath(path);
            var directoryToCheck = "";
            var directories = fullPath.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            directories.Remove(directories.Last()); // last element will be the file name, not a directory
            foreach (var directory in directories)
            {
                directoryToCheck = directoryToCheck != ""
                    ? string.Format("{0}/{1}", directoryToCheck, directory)
                    : directory;
                if (!Directory.Exists(directoryToCheck))
                    Directory.CreateDirectory(directoryToCheck);
            }

            // create the file
            using (var fileStream = File.Create(fullPath))
            {
                try
                {
                    fileStream.Write(data, 0, data.Length);
                }
                finally
                {
                    fileStream.Close();
                }
            }
        }

        public byte[] Get(string path)
        {
            path.ValidateAsBinaryStoragePath();

            // return null when file does not exist
            if (!Exists(path)) return null;

            var fullPath = GetFullPath(path);
            var data = File.ReadAllBytes(fullPath);
            return data;
        }

        public void Delete(string path)
        {
            path.ValidateAsBinaryStoragePath();

            // do nothing unless file exists
            if (!Exists(path)) return;

            var fullPath = GetFullPath(path);
            File.Delete(fullPath);
        }
    }
}
