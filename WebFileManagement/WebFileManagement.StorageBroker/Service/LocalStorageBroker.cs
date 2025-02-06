using System.IO.Compression;

namespace WebFileManagement.StorageBroker.Service;

public class LocalStorageBroker : IStorageBroker
{

    private readonly string _dataPath;

    public LocalStorageBroker()
    {
        _dataPath = Path.Combine(Directory.GetCurrentDirectory(), "data");

        if (!Directory.Exists(_dataPath))
        {
            Directory.CreateDirectory(_dataPath);
        }

    }

    public async Task CreateFolderAsync(string folderPath)
    {

        folderPath = Path.Combine(_dataPath, folderPath);
        if (Directory.Exists(folderPath))
        {
            throw new Exception("directory has already created");
        }
        var parentPath = Directory.GetParent(folderPath);

        if (!Directory.Exists(parentPath.FullName))
        {
            throw new Exception("Parent path not found");
        }
        Directory.CreateDirectory(folderPath);

    }

    public async Task DeleteFolderAsync(string folderPath)
    {
        folderPath = Path.Combine(_dataPath, folderPath);
        if (!Directory.Exists(_dataPath))
        {
            throw new Exception("not found");
        }
        var parentPath = Directory.GetParent(folderPath);

        if (!Directory.Exists(parentPath.FullName))
        {
            throw new Exception("Parent path not found");
        }
        Directory.Delete(folderPath, true);
    }

    public async Task DeleteFileAsync(string filePath)
    {
        filePath = Path.Combine(_dataPath, filePath);
        if (!Directory.Exists(filePath))
        {
            throw new Exception("delete file not found");
        }
        var parentPath = Directory.GetParent(filePath);

        if (!Directory.Exists(parentPath.FullName))
        {
            throw new Exception("Parent path not found");
        }
        File.Delete(filePath);
    }

    public async Task<Stream> DownloadFileAsync(string filePath)
    {
        filePath = Path.Combine(_dataPath, filePath);

        if (!File.Exists(filePath))
        {
            throw new Exception(" not found");

        }

        return new FileStream(filePath, FileMode.Open, FileAccess.Read);

    }

    public async Task<Stream> DownloadFolderAzZipAsync(string folderPath)
    {
        var path = Path.Combine(_dataPath, folderPath);
        if (Path.GetExtension(path) != string.Empty)
        {
            throw new Exception("folder pas not as a folder");
        }

        if (!Directory.Exists(path))
        {
            throw new Exception("not found");
        }

        var ziPath = path + ".zip";

        ZipFile.CreateFromDirectory(path, ziPath);

        return new FileStream(ziPath, FileMode.Open, FileAccess.Read);



    }

    public async Task<List<string>> GetAllInFolderPathAsync(string folderPath)
    {
        folderPath = Path.Combine(_dataPath, folderPath);
        if (!Directory.Exists(folderPath))
        {
            throw new Exception("not found");
        }
        var parentPath = Directory.GetParent(folderPath);

        if (!Directory.Exists(parentPath.FullName))
        {
            throw new Exception("parentPath not found");
        }

        var getAll = Directory.GetFileSystemEntries(folderPath).ToList();

        getAll = getAll.Select(p => p.Remove(0, folderPath.Length + 1)).ToList();
        return getAll;
    }

    public async Task UploadFileAsync(string directoryPath, Stream stream)
    {
        directoryPath = Path.Combine(_dataPath, directoryPath);
        if (File.Exists(directoryPath))
        {
            throw new Exception("already uploaded");

        }

        using (var filestream = new FileStream(directoryPath, FileMode.Create, FileAccess.Write))
        {
            stream.CopyTo(filestream);
        }


    }

    public async Task UploadFileWithChunksAsync(string directoryPath, Stream stream)
    {
        directoryPath = Path.Combine(_dataPath, directoryPath);
        if (File.Exists(directoryPath))
        {
            throw new Exception("already uploaded");

        }

        var buffer = new byte[1024 * 1024 * 10];

        using (var filestream = new FileStream(directoryPath, FileMode.Create, FileAccess.Write))
        {
            while (true)
            {
                var res = stream.Read(buffer, 0, buffer.Length);
                if (res <= 0)
                {
                    break;
                }
                filestream.Write(buffer, 0, res);
            }

        }
    }

}
