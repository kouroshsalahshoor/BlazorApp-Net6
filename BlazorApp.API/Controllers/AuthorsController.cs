using AutoMapper;
using BlazorApp.API.Data;
using BlazorApp.API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

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
        public async Task<ActionResult<IEnumerable<AuthorDto>>> Get()
        {
            try
            {
                var models = await _db.Authors.ToListAsync();
                //var models = await _db.Authors.Include(x => x.Books).ToListAsync();
                return Ok(_mapper.Map<IEnumerable<AuthorDto>>(models));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $">>> Error in Author {nameof(Get)}");
                return StatusCode(500, ApiErrorMessages.Error500);
            }
        }

        // GET: api/Authors/5
        [HttpGet("/api/author/{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> Get(int id)
        {
            try
            {
                var model = await _db.Authors.FirstOrDefaultAsync(x => x.Id == id);
                //var model = await _db.Authors.Include(x => x.Books).FirstOrDefaultAsync(x => x.Id == id);

                if (model == null)
                {
                    return NotFound();
                }

                return _mapper.Map<AuthorDto>(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $">>> Error in Author {nameof(Get)} with Id");
                return StatusCode(500, ApiErrorMessages.Error500);
            }
        }

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

            var model = await _db.Authors.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _db.Entry(model).State = EntityState.Detached;//!!!

            model = _mapper.Map<Author>(dto);
            _db.Entry(model).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            //catch (DbUpdateConcurrencyException ex)
            {
                if (!await Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, $">>> Error in Author {nameof(Put)}");
                    return StatusCode(500, ApiErrorMessages.Error500);
                }
            }

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/api/author")]
        //[HttpPost]
        [Authorize(Roles = Roles.Admins)]
        public async Task<ActionResult<AuthorCreateEditDto>> Post(AuthorCreateEditDto dto)
        {
            var model = _mapper.Map<Author>(dto);
            await _db.Authors.AddAsync(model);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $">>> Error in Author {nameof(Post)}");
                return StatusCode(500, ApiErrorMessages.Error500);
            }

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
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
                if (model.Books.Any())
                {
                    foreach (var item in model.Books)
                    {
                        _db.Books.Remove(item);
                    }
                }
                _db.Authors.Remove(model);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $">>> Error in Author {nameof(Delete)}");
                return StatusCode(500, ApiErrorMessages.Error500);
            }

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        private async Task<bool> Exists(int id)
        {
            return await _db.Authors.AnyAsync(e => e.Id == id);
        }
    }
}
