namespace WebFileManagement.Service.Service;

public interface IWebFileManagementService
{

    Task CreateFolderAsync(string folderPath);
    Task DeleteFolderAsync(string folderPath);
    Task DeleteFileAsync(string filePath);
    Task<Stream> DownloadFileAsync(string filePath);
    Task<Stream> DownloadFolderAzZipAsync(string folderPath);
    Task<List<string>> GetAllInFolderPathAsync(string folderPath);
    Task UploadFileAsync(string directoryPath, Stream stream);
    Task UploadFileWithChunksAsync(string directoryPath, Stream stream);

}