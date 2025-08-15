using AEspejo.FileLoaderService.Enums;

namespace AEspejo.FileLoaderService.Models
{
    public class FileResult
    {
        public string FileName { get; set; } = string.Empty;
        public byte[] Content { get; set; } = Array.Empty<byte>();
        public FileTypeEnum Source { get; set; }
    }
}
