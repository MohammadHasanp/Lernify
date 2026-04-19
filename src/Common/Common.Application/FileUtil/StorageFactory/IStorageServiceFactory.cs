namespace Common.Application.FileUtil.StorageFactory;

using Common.Application.FileUtil.Enums;
using Common.Application.FileUtil.StorageInterfaces;

public interface IStorageServiceFactory
{
    public IStorageService GetStorageService(EnStorageType storageType);
}
