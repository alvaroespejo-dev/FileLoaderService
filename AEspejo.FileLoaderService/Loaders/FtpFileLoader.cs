using AEspejo.FileLoaderService.Enums;
using AEspejo.FileLoaderService.Interfaces;
using AEspejo.FileLoaderService.Models;
using System.Net;

namespace AEspejo.FileLoaderService.Loaders
{
    public class FtpFileLoader : IFileLoader
    {
        private readonly string _ftpUser;
        private readonly string _ftpPassword;

        public FtpFileLoader(string ftpUser, string ftpPassword)
        {
            _ftpUser = ftpUser;
            _ftpPassword = ftpPassword;
        }

        public async Task<FileResult> LoadAsync(string sourcePath)
        {
            var uri = new Uri(sourcePath);
            using var handler = new HttpClientHandler
            {
                Credentials = new NetworkCredential(_ftpUser, _ftpPassword)
            };
            using var client = new HttpClient(handler);

            using var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);

            return new FileResult
            {
                FileName = Path.GetFileName(sourcePath),
                Content = ms.ToArray(),
                Source = FileTypeEnum.FTP
            };
        }
    }
}
