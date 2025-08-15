using AEspejo.FileLoaderService.Models;

namespace AEspejo.FileLoaderService.Interfaces
{
    public interface IFileLoader
    {
        Task<FileResult> LoadAsync(string source);
    }
}
