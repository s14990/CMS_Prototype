using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMS_Prototype.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace CMS_Prototype.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly cmsmainContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public VideosController(cmsmainContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: api/Videos
        [HttpGet]
        public IEnumerable<Models.Video> GetVideo()
        {
            return _context.Video;
        }

        // GET: api/Videos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVideo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var video = await _context.Video.FindAsync(id);

            if (video == null)
            {
                return NotFound();
            }

            return Ok(video);
        }

        // PUT: api/Videos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideo([FromRoute] int id, [FromBody] Models.Video video)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != video.Id)
            {
                return BadRequest();
            }

            _context.Entry(video).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Videos
        [HttpPost]
        public async Task<IActionResult> PostVideo([FromForm] UploadFile model)
        {
            var file = model.File;

                Account account = new Account(
                "dn2pht7no",
                "763416155661231",
                "Y8_D6HDCOUNJAUoPvi8wtVWhkmE");

                Cloudinary cloudinary = new Cloudinary(account);

            if (file.Length > 0)
            {
                string path = Path.Combine(_hostingEnvironment.ContentRootPath,@"ClientApp\src\components", "uploadFiles");
                using (var fs = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
                string path2 = Path.Combine(path, file.FileName);
                var uploadParams = new VideoUploadParams()
                {
                    File = new FileDescription(path2)
                };
                var uploadResult = cloudinary.Upload(uploadParams);

                model.source = $"/uploadFiles{file.FileName}";
                Models.Video v = new Models.Video();
                v.VideoName = file.FileName;
                v.VideoLink = uploadResult.Uri.ToString();
                v.Format= Path.GetExtension(file.FileName).Substring(1).Trim();
                _context.Video.Add(v);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetVideo", new { id = v.Id }, v);

                
            }
            return BadRequest();
        }

        // DELETE: api/Videos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var video = await _context.Video.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }

            _context.Video.Remove(video);
            await _context.SaveChangesAsync();

            return Ok(video);
        }

        private bool VideoExists(int id)
        {
            return _context.Video.Any(e => e.Id == id);
        }
    }
}