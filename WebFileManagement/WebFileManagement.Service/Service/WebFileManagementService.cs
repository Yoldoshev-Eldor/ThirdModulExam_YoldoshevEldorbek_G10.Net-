using WebFileManagement.StorageBroker.Service;

namespace WebFileManagement.Service.Service;

public class WebFileManagementService : IWebFileManagementService
{
    private readonly IStorageBroker _storageBroker;

    public WebFileManagementService(IStorageBroker storageBroker)
    {
        _storageBroker = storageBroker;
    }

    public async Task CreateFolderAsync(string folderPath)
    {
        await _storageBroker.CreateFolderAsync(folderPath);
    }

    public async Task DeleteFileAsync(string filePath)
    {
        await _storageBroker.DeleteFileAsync(filePath);
    }

    public async Task DeleteFolderAsync(string folderPath)
    {
        _storageBroker.DeleteFolderAsync(folderPath);
    }

    public async Task<Stream> DownloadFileAsync(string filePath)
    {
        return await _storageBroker.DownloadFileAsync(filePath);
    }

    public async Task<Stream> DownloadFolderAzZipAsync(string folderPath)
    {
        return await _storageBroker.DownloadFolderAzZipAsync(folderPath);
    }

    public async Task<List<string>> GetAllInFolderPathAsync(string folderPath)
    {
        return await _storageBroker.GetAllInFolderPathAsync(folderPath);
    }

    public async Task UploadFileAsync(string directoryPath, Stream stream)
    {
        await _storageBroker.UploadFileAsync(directoryPath, stream);
    }

    public async Task UploadFileWithChunksAsync(string directoryPath, Stream stream)
    {
         await _storageBroker.UploadFileWithChunksAsync(directoryPath, stream);
    }
}
