using AutoMapper;
using BlazorApp.API.Data;
using BlazorApp.API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(ApplicationDbContext db, IMapper mapper, ILogger<AuthorsController> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetList()
        {
            try
            {
                return await _db.Authors.Include(x => x.Books).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $">>> Error in {nameof(GetList)}");
                return StatusCode(500, ApiErrorMessages.Error500);
            }
        }

        // GET: api/Authors/5
        [HttpGet("/api/author/{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<Author>> Get(int id)
        {
            var author = await _db.Authors.Include(x => x.Books).FirstOrDefaultAsync(x => x.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        //[HttpGet("/api/author/2/{id}")]
        //public async Task<ActionResult<Author>> GetAuthor2(int id)
        //{
        //    var author = await _db.Authors.FindAsync(id);

        //    if (author == null)
        //    {
        //        return NotFound();
        //    }

        //    return author;
        //}

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("/api/author/{id}")]
        //[HttpPut("{id}")]
        [Authorize(Roles = Roles.Admins)]
        public async Task<IActionResult> Put(int id, AuthorCreateEditDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var model = await _db.Authors.SingleOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, model);

            _db.Entry(model).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            //catch (DbUpdateConcurrencyException ex)
            {
                if (!exists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, $">>> Error in {nameof(Put)}");
                    return StatusCode(500, ApiErrorMessages.Error500);
                }
            }

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
            //return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/api/author")]
        //[HttpPost]
        [Authorize(Roles = Roles.Admins)]
        public async Task<ActionResult<Author>> Post(AuthorCreateEditDto dto)
        {
            var model = _mapper.Map<Author>(dto);
            _db.Authors.Add(model);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $">>> Error in {nameof(Put)}");
                return StatusCode(500, ApiErrorMessages.Error500);
            }

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
            //return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("/api/author/{id}")]
        //[HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admins)]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _db.Authors.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            try
            {
                _db.Authors.Remove(model);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $">>> Error in {nameof(Delete)}");
                return StatusCode(500, ApiErrorMessages.Error500);
            }

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        private bool exists(int id)
        {
            return _db.Authors.Any(e => e.Id == id);
        }
    }
}
