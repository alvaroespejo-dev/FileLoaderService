using AEspejo.FileLoaderService.Enums;
using AEspejo.FileLoaderService.Interfaces;
using AEspejo.FileLoaderService.Models;

namespace AEspejo.FileLoaderService.Loaders
{
    public class LocalFileLoader : IFileLoader
    {
        public async Task<FileResult> LoadAsync(string sourcePath)
        {
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("File not found", sourcePath);

            var bytes = await File.ReadAllBytesAsync(sourcePath);
            return new FileResult
            {
                FileName = Path.GetFileName(sourcePath),
                Content = bytes,
                Source = FileTypeEnum.LOCAL
            };
        }
    }
}
