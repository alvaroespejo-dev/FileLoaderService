using AEspejo.FileLoaderService.Enums;
using AEspejo.FileLoaderService.Interfaces;
using AEspejo.FileLoaderService.Models;

namespace AEspejo.FileLoaderService.Loaders
{
    public class HttpFileLoader : IFileLoader
    {
        private readonly HttpClient _httpClient = new();

        public async Task<FileResult> LoadAsync(string sourcePath)
        {
            var bytes = await _httpClient.GetByteArrayAsync(sourcePath);
            return new FileResult
            {
                FileName = Path.GetFileName(sourcePath),
                Content = bytes,
                Source = FileTypeEnum.HTTP
            };
        }
    }
}
