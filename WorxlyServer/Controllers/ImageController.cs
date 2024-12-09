using Microsoft.AspNetCore.Mvc;

namespace WorxlyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private List<string> allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "uploads/images")))
                Directory.CreateDirectory("uploads/images");
            string extension = Path.GetExtension(file.FileName).ToLower();
            string filename = Guid.NewGuid() + extension;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "uploads/images", filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { Filename = filename});
        }
        [HttpGet("{filename}")]
        public async Task<IActionResult> GetImage(string filename)
        {
            string extension = Path.GetExtension(filename).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Invalid file extension");
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "uploads/images", filename);
            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }
            extension.Remove(0, 1);
            return PhysicalFile(path, $"image/{extension}");
        }
    }
}
