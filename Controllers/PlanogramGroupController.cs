using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanogramBackend.Data;
using PlanogramBackend.Models;

namespace PlanogramBackend.Controllers
{
    [ApiController]
    [Route("api/planogram-groups")]
    public class PlanogramGroupController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlanogramGroupController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] PlanogramGroup group)
        {
            _context.PlanogramGroups.Add(group);
            await _context.SaveChangesAsync();
            return Ok(group);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanogramGroup>>> GetGroups()
        {
            return await _context.PlanogramGroups
                .Include(g => g.ProductImages)
                .ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _context.PlanogramGroups.FindAsync(id);
            if (group == null) return NotFound();

            _context.PlanogramGroups.Remove(group);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
