using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Services.Utils;

namespace Services.Service;

public class FileUploadService(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor) : IFileUploadService
{
    public string ImageUpload(IFormFile file, string directoryName)
    {
        var (filePath,fileName) = FilePaths.BuildPath(Path.GetFileNameWithoutExtension(file.FileName), Path.GetExtension(file.FileName), directoryName, webHostEnvironment);

        using(var fileStream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(fileStream);
        }

        return $"{httpContextAccessor.HttpContext!.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host.Value}/{directoryName}/{fileName}";
    }
}
