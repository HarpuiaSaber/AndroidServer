using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.API
{
    [Route("api/Dowload/[action]")]
    [ApiController]
    public class DownloadAPIController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public DownloadAPIController( IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string path)
        {
            var image = System.IO.File.OpenRead(path);
            return File(image, "image/jpeg");
        }

        [HttpGet]
        public async Task<IActionResult> GetImage(string path)
        {
            var imageString = System.IO.File.OpenRead(Path.Combine(_hostingEnvironment.WebRootPath, "images", path));
            return File(imageString, "image/**");
        }
         
    }
}