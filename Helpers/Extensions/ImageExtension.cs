using System;
namespace FinalProject.Helpers.Extensions;
public static class ImageExtension
{
    public static bool CheckFileType(this IFormFile file, string fileType)
    {
        return file.ContentType.Contains(fileType);
    }
    public static bool CheckFileSize(this IFormFile file, int size)
    {
        return file.Length / 1024 > size;
    }
}


