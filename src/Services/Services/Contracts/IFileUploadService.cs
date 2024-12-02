namespace Services.Contracts;

public interface IFileUploadService
{
    string ImageUpload(IFormFile file, string directoryName);
    
}


