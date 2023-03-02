using AutoMapper;
using BlazorApp.API.Data;
using BlazorApp.API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BlazorApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(ApplicationDbContext db, IMapper mapper, ILogger<BooksController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> Get()
        {
            try
            {
                var models = await _db.Books.ToListAsync();
                //var models = await _db.Books.Include(x => x.Author).ToListAsync();
                return Ok(_mapper.Map<IEnumerable<BookDto>>(models));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $">>> Error in Book {nameof(Get)}");
                return StatusCode(500, ApiErrorMessages.Error500);
            }
        }

        [HttpGet("/api/book/{id}")]
        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            try
            {
                var model = await _db.Books.SingleOrDefaultAsync(x => x.Id == id);
                //var model = await _db.Books.Include(x => x.Author).SingleOrDefaultAsync(x => x.Id == id);

                if (model == null)
                {
                    return NotFound();
                }

                var dto = _mapper.Map<BookDto>(model);

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $">>> Error in Book {nameof(GetById)}");
                return StatusCode(500, ApiErrorMessages.Error500);
            }
        }

        [HttpPut("/api/book/{id}")]
        [Authorize(Roles = Roles.Admins)]
        public async Task<IActionResult> Put(int id, BookCreateEditDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var model = await _db.Books.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _db.Entry(model).State = EntityState.Detached;
            
            model = _mapper.Map<Book>(dto);

            _db.Entry(model).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!await Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, $">>> Error in Book {nameof(Put)}");
                    return StatusCode(500, ApiErrorMessages.Error500);
                }
            }

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPost("/api/book")]
        [Authorize(Roles = Roles.Admins)]
        public async Task<ActionResult<Book>> Post(BookCreateEditDto dto)
        {
            var model = _mapper.Map<Book>(dto);
            model.Image = saveImage(dto.Image, dto.ImageName);

            _db.Entry(model).State = EntityState.Detached;
            
            await _db.Books.AddAsync(model);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $">>> Error in Book {nameof(Put)}");
                return StatusCode(500, ApiErrorMessages.Error500);
            }

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpDelete("/api/book/{id}")]
        [Authorize(Roles = Roles.Admins)]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _db.Books.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            try
            {
                _db.Books.Remove(model);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $">>> Error in Book {nameof(Delete)}");
                return StatusCode(500, ApiErrorMessages.Error500);
            }

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        private async Task<bool> Exists(int id)
        {
            return await _db.Books.AnyAsync(e => e.Id == id);
        }

        private string saveImage(string base64, string imageName)
        {
            var url = HttpContext.Request.Host.Value;
            var ext = Path.GetExtension(imageName);
            var fileName = $"{Guid.NewGuid()}.{ext}";
            var path = _webHostEnvironment.WebRootPath + "\\images\\" + fileName;

            byte[] image = Convert.FromBase64String(base64);

            var fileStream = System.IO.File.Create(path);
            fileStream.Write(image, 0, image.Length);
            fileStream.Close();

            return $"https://{url}/images/{fileName}";
        }
    }
}
