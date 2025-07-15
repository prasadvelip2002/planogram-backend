using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanogramBackend.Data;
using PlanogramBackend.Models;

namespace PlanogramBackend.Controllers
{
    [ApiController]
    [Route("api/planogram-layout")]
    public class PlanogramController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string[] allowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];

        public PlanogramController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlanogram()
        {
            var groups = await _context.PlanogramGroups
                .Include(g => g.ProductImages)
                .ToListAsync();

            var groupedByRow = groups
                .GroupBy(g => g.ShelfRow)
                .OrderBy(g => g.Key)
                .Select(rowGroup => rowGroup.Select(g => new
                {
                    label = g.Label,
                    productImageUrls = g.ProductImages.Select(p => ResolveImageUrl(p.ImageUrl)).ToList()
                }).ToList())
                .ToList();

            return Ok(groupedByRow);
        }

        private string ResolveImageUrl(string filename)
        {
            var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (Path.HasExtension(filename))
            {
                var fullPath = Path.Combine(wwwRootPath, filename);
                if (System.IO.File.Exists(fullPath))
                    return "/images/" + filename;
            }

            foreach (var ext in allowedExtensions)
            {
                var testPath = Path.Combine(wwwRootPath, filename + ext);
                if (System.IO.File.Exists(testPath))
                    return "/images/" + filename + ext;
            }

            return "/images/notfound.png";
        }
    }
}
