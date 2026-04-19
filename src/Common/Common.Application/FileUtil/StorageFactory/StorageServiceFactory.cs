namespace Common.Application.FileUtil.StorageFactory;


using Common.Application.FileUtil.Enums;
using Common.Application.FileUtil.StorageInterfaces;
using Common.Application.FileUtil.StorageServices;

public class StorageServiceFactory : IStorageServiceFactory
{
    public IStorageService GetStorageService(EnStorageType storageType)
    {
        return storageType switch
        {
            EnStorageType.File => new FileStorageService(),
            _ => new FileStorageService(),

            //EnStorageType.FTP => new FileStorageService(),
            //_ => new FileStorageService(),
        };
    }
}
