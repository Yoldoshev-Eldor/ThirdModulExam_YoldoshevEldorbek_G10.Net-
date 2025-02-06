using Microsoft.AspNetCore.Mvc;
using WebFileManagement.Service.Service;

namespace WebFileManagement.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WebFileManagementController : ControllerBase
{
    private readonly IWebFileManagementService _server;

    public WebFileManagementController(IWebFileManagementService server)
    {
        _server = server;
    }

    [HttpPost("createFolder")]
    public async Task CreateDirectory(string folderPath)
    {
        await _server.CreateFolderAsync(folderPath);
    }
    [HttpDelete("deleteFolder")]
    public async Task DeleteDirectory(string folderPath)
    {
        await _server.DeleteFolderAsync(folderPath);
    }


    [HttpGet("getAll")]

    public async Task<List<string>> GetAll(string? path)
    {
        path = path ?? string.Empty;
        return await _server.GetAllInFolderPathAsync(path);
    }


    [HttpPost("uploadFile")]
    public async Task UploadFile(IFormFile file, string? folderPath)
    {

        folderPath = folderPath ?? string.Empty;
        folderPath = Path.Combine(folderPath, file.FileName);

        using (var stream = file.OpenReadStream())
        {
            await _server.UploadFileAsync(folderPath, stream);
        }
    }

     [HttpPost("uploadFileChunks")]
    public async Task UploadFileChunks(IFormFile file, string? folderPath)
    {

        folderPath = folderPath ?? string.Empty;
        folderPath = Path.Combine(folderPath, file.Name);

        using (var stream = file.OpenReadStream())
        {
            await _server.UploadFileWithChunksAsync(folderPath, stream);
        }
    }


    [HttpPost("uploadFiles")]
    public async Task UploadFiles(List<IFormFile> files, string folderPath)
    {
        folderPath = folderPath ?? string.Empty;

        if (files.Count == 0 || string.IsNullOrEmpty(folderPath))
        {
            throw new Exception("file is null or empty");
        }

        foreach (var file in files)
        {
            folderPath = Path.Combine(folderPath, file.FileName);

            using (var stream = file.OpenReadStream())
            {
                await _server.UploadFileAsync(folderPath, stream);
            }


        }

    }
    [HttpGet("downloadFile")]
    public async Task<FileStreamResult> DownlodaFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new Exception("file null or empty");
        }

        var fileName = Path.GetFileName(filePath);
        var stream = await _server.DownloadFileAsync(fileName);

        var res = new FileStreamResult(stream, "application/octet-stream")
        {
            FileDownloadName = fileName,
        };
        return res;

    }

    [HttpGet("downloadFileAzZip")]
    public async Task<FileStreamResult> DownloadFileAzZip(string folderPath)
    {
        var zipPath = folderPath + ".zip";
        var stream = await _server.DownloadFolderAzZipAsync(zipPath);

        var res = new FileStreamResult(stream, "application/octet-stream")
        {
            FileDownloadName = zipPath,
        };
        return res;
    }

}
