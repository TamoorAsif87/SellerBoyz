using Microsoft.AspNetCore.Hosting;

namespace Services.Utils;

public static class FilePaths
{
    public static (string,string) BuildPath(string fileName,string fileExtension,string directoryName,IWebHostEnvironment webHostEnvironment)
    {
        string newFileName = $"{fileName}-{Guid.NewGuid()}{fileExtension}";
        string directoryPath = webHostEnvironment.WebRootPath + $@"\{directoryName}";

        return (Path.Combine(directoryPath,newFileName), newFileName);
    }

    public static void RemoveFile(string fileAddress)
    {

    }
}
