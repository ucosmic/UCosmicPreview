using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using UCosmic.Impl.BinaryData;

namespace UCosmic.BinaryData
{
    // stores binary data as blobs in azure storage account
    public class AzureBlobBinaryDataStorage : IStoreBinaryData
    {
        private readonly CloudBlobClient _blobClient;

        public AzureBlobBinaryDataStorage(string connectionStringName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionString == null)
                throw new InvalidOperationException(string.Format(
                    "There is no ConnectionString named '{0}' in the configuration file.", connectionStringName));

            if (string.IsNullOrWhiteSpace(connectionString.ConnectionString))
                throw new InvalidOperationException(string.Format(
                    "The ConnectionString named '{0}' has no connectionString attribute value.", connectionStringName));

            var storageAccount = CloudStorageAccount.Parse(connectionString.ConnectionString);
            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        private static string GetContainerName(string path)
        {
            var pathParts = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            return pathParts.First();
        }

        private CloudBlobContainer GetContainer(string path)
        {
            var containerName = GetContainerName(path);
            var container = _blobClient.GetContainerReference(containerName);
            return container;
        }

        private static string GetBlobName(string path)
        {
            var pathParts = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var fileName = string.Join("/", pathParts.Skip(1));
            return fileName;
        }

        private CloudBlockBlob GetBlob(string path, CloudBlobContainer container = null)
        {
            container = container ?? GetContainer(path);
            var blobName = GetBlobName(path);
            var blob = container.GetBlockBlobReference(blobName);
            return blob;
        }

        public bool Exists(string path)
        {
            path.ValidateAsBinaryStoragePath();

            var container = GetContainer(path);
            var blob = GetBlob(path, container);

            return container.Exists() && blob.Exists();
        }

        public void Put(string path, byte[] data, bool overwrite = false)
        {
            path.ValidateAsBinaryStoragePath();

            // disallow file replacement unless specified in method invocation
            if (!overwrite && Exists(path))
                throw new InvalidOperationException(string.Format(
                    "A file already exists at the path '{0}'. To overwrite this file, invoke this method with overwrite == true.", path));

            var container = GetContainer(path);

            // for reference only. NEVER make these blobs public.
            //containerReference.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            container.CreateIfNotExists();
            var blob = GetBlob(path, container);

            using (var stream = new MemoryStream(data, 0, data.Length))
            {
                blob.UploadFromStream(stream);
            }
        }

        public byte[] Get(string path)
        {
            path.ValidateAsBinaryStoragePath();

            // return null when file does not exist
            if (!Exists(path)) return null;

            var blob = GetBlob(path);

            using (var stream = new MemoryStream())
            {
                blob.DownloadToStream(stream);
                return stream.ToArray();
            }
        }

        public void Delete(string path)
        {
            path.ValidateAsBinaryStoragePath();

            // do nothing unless file exists
            if (!Exists(path)) return;

            var containerName = GetContainerName(path);
            var blobName = GetBlobName(path);
            var container = _blobClient.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(blobName);

            blob.Delete();
        }
    }
}
