using System.Configuration;
using SimpleInjector;
using UCosmic.BinaryData;

namespace UCosmic.Impl.BinaryData
{
    public static class SimpleInjectorBinaryDataStorageRegistration
    {
        public static void RegisterBinaryDataStorage(this Container container, ContainerConfiguration configuration)
        {
            if (configuration.IsDeployedToCloud)
            {
                container.Register<IStoreBinaryData>(() => new AzureBlobBinaryDataStorage("UCosmicCloudData"));
            }
            else
            {
                // register azure if connection string is set up to point to development storage
                var connectionString = ConfigurationManager.ConnectionStrings["UCosmicCloudData"];
                if (connectionString != null && connectionString.ConnectionString == "UseDevelopmentStorage=true")
                    container.Register<IStoreBinaryData>(() => new AzureBlobBinaryDataStorage("UCosmicCloudData"));

                else
                    container.Register<IStoreBinaryData>(() => new IisFileSystemBinaryDataStorage("~/App_Data/binary-data"));
            }
        }
    }
}
