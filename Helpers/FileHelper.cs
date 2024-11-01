namespace panasonic.Helpers;

public interface IFileHelper
{
    Task<string> SaveFile(IFormFile file, params string[] paths);
    void DeleteFile(string fileName, params string[] paths);
}

public class FileHelper : IFileHelper
{
    public async Task<string> SaveFile(IFormFile file, params string[] paths)
    {

        if (file == null || file.Length == 0) throw new ArgumentException("File cannot be null or empty", nameof(file));

        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "storage", Path.Combine(paths));

        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        string filePath = Path.Combine(uploadPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
    }

    public void DeleteFile(string fileName, params string[] paths)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "storage", Path.Combine(paths), fileName);
        File.Delete(filePath);
    }

}