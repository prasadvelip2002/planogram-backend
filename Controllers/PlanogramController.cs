using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanogramBackend.Data;
using PlanogramBackend.Models;

namespace PlanogramBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanogramController(AppDbContext context) : ControllerBase
    {
        private readonly string[] allowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];

        [HttpGet]
        public async Task<IActionResult> GetPlanogram()
        {
            var groups = await context.PlanogramGroups
                .Include(g => g.ProductImages)
                .ToListAsync();

            // ✅ Group by ShelfRow for row-wise rendering
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

        // ✅ Automatically resolve file extension (if DB has no extension or wrong one)
        private string ResolveImageUrl(string filename)
        {
            var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            // If extension already exists in filename (e.g., 'moist.jpg'), check directly
            if (Path.HasExtension(filename))
            {
                var fullPath = Path.Combine(wwwRootPath, filename);
                if (System.IO.File.Exists(fullPath))
                    return "/images/" + filename;
            }

            // Try all extensions
            foreach (var ext in allowedExtensions)
            {
                var testPath = Path.Combine(wwwRootPath, filename + ext);
                if (System.IO.File.Exists(testPath))
                    return "/images/" + filename + ext;
            }

            // Fallback image
            return "/images/notfound.png"; // Optional: add a default 'notfound.png' in wwwroot/images
        }
    }
}
