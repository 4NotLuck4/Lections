using Lection1024.Contexts;
using Lection1024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lection1024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(GamesDbContext context) : ControllerBase
    {
        private readonly GamesDbContext _context = context;

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories(
            [FromQuery] string? sortBy = null ,
            [FromQuery] int? page = null)
        {
            var categories = _context.Categories.AsQueryable();

            if (string.IsNullOrWhiteSpace(sortBy))       // string != nul / ""
            {
                categories = sortBy?.ToLower() switch
                {
                    "name" => categories.OrderBy(c => c.Name),
                    "id" => categories.OrderBy(c => c.CategoryId),
                    _ => categories
                };
            }

            if (page.HasValue) // int != null
            {
                var pageSize = 10;

                categories = categories
                    .Skip(pageSize * ((int)page - 1))
                    .Take(pageSize);
            }

            return await categories.ToListAsync();
        }

        [HttpGet("{category}/games")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByCategory(
            [FromQuery] string? category)
            => await _context.Games
                .Where(g => g.Category.Name == category)
                .ToListAsync();

        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<Category>>> GetFilteredCategories(
            [FromQuery] string? category)
            => await _context.Categories
                .Where(c => c.Name.Contains(category ?? ""))
                .ToListAsync();

        // GET: api/Categories/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory([FromRoute] int id)
        {
            var category = await _context.Categories.FindAsync(id);

            return category is null ? NotFound() : category;
        }

        // PUT: api/Categories/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutCategory([FromRoute] int id, [FromBody] Category category)
        {
            if (id != category.CategoryId)
                return BadRequest();

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoryExistsAsync(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null)
                return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CategoryExistsAsync(int id)
            => await _context.Categories.AnyAsync(e => e.CategoryId == id);
    }
}