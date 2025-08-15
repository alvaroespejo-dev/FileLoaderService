using AEspejo.FileLoaderService.Interfaces;
using AEspejo.FileLoaderService.Loaders;
using Microsoft.AspNetCore.Mvc;

namespace AEspejo.FileLoaderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileLoaderController : Controller
    {
        private async Task<IActionResult> LoadFileAsync(IFileLoader loader, string path)
        {
            if (loader == null)
                return BadRequest("Invalid source type");

            try
            {
                var fileResult = await loader.LoadAsync(path);
                if (fileResult == null)
                    return NotFound("File not found or empty");

                return Ok(new
                {
                    fileResult.FileName,
                    Base64Content = Convert.ToBase64String(fileResult.Content),
                    fileResult.Source
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("LocalFile")]
        public Task<IActionResult> LocalFile(string path) =>
            LoadFileAsync(new LocalFileLoader(), path);

        [HttpGet("HttpFile")]
        public Task<IActionResult> HttpFile(string path) =>
            LoadFileAsync(new HttpFileLoader(), path);

        [HttpGet("FtpFile")]
        public Task<IActionResult> FtpFile(string user, string password, string path) =>
            LoadFileAsync(new FtpFileLoader(user, password), path);

        [HttpGet("EmailFile")]
        public Task<IActionResult> EmailFile(string host, int port, string username, string password, string? fromEmail, string? subject) =>
            LoadFileAsync(new EmailFileLoader(host, port, username, password, fromEmail, subject), string.Empty);
    }
}
