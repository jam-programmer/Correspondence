using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace Application.Common.Extension;

internal static class FileProcessing
{

    public static string UploadImage
        (this IFormFile file,
        string folder,
        string? defualt = null)
    {
        if (file is not null)
        {
            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "Gallery", folder);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string filePath = Path.Combine(folderPath, fileName);
            var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream());
            var save = image.SaveAsWebpAsync(filePath);
            if (save.IsCompleted)
            {
                return fileName;
            }
        }
        if (!string.IsNullOrEmpty(defualt))
        {
            return defualt;
        }
        return "notFound.jpg";
    }

    public static async Task<string> UploadFileAsync(
       this IFormFile file,
       string folder,
       string? defaultFileName = null)
    {
        if (file != null && file.Length > 0)
        {
            
            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "File", folder);

          
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, fileName);

          
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

         
            if (file.ContentType.StartsWith("image/"))
            {
                var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream());
                var webpFilePath = Path.ChangeExtension(filePath, ".webp");
                await image.SaveAsWebpAsync(webpFilePath);
                return fileName; 
            }

            return fileName; 
        }

       
        return !string.IsNullOrEmpty(defaultFileName) ? defaultFileName : "notFound.jpg";
    }
    public static BaseResult RemoveImage(this string fileName,
        string folder, string? defualt = null)
    {
        if (fileName == "defaultAvatar.jpg" || fileName == "notFound.jpg")
        {
            return BaseResult.Success();
        }
        if (!string.IsNullOrEmpty(defualt) && fileName == defualt)
        {
            return BaseResult.Success();
        }

        string folderPath = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot", "Gallery", folder);

        string path = Path.Combine(folderPath, fileName);

        if (File.Exists(path))
        {
            File.Delete(path);
            return BaseResult.Success();
        }
        else
        {
            return BaseResult.Fail("");
        }
    }
}

   

