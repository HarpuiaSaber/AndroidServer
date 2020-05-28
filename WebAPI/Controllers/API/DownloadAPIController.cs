using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.API
{
    [Route("api/Dowload/[action]")]
    [ApiController]
    public class DownloadAPIController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(string path)
        {
            var image = System.IO.File.OpenRead(path);
            return File(image, "image/jpeg");
        }
    }
}