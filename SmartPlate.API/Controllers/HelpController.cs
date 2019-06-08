using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace SmartPlate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpController : ControllerBase
    {
        private readonly IHostingEnvironment _env;

        public HelpController(IHostingEnvironment env)
        {
            _env = env;
        }

        [HttpGet("release")]
        public IActionResult GetReleaseNotes()
        {
            var path = Path.Combine(_env.ContentRootPath, "Release Notes.txt");
            if (!System.IO.File.Exists(path))
                path = "Release Notes.txt";
            var text = System.IO.File.ReadAllText(path);
            return Ok(text);
        }
    }
}