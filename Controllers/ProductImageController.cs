using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanogramBackend.Data;
using PlanogramBackend.Models;

namespace PlanogramBackend.Controllers;

[ApiController]
[Route("api/product-images")]
public class ProductImageController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductImageController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddImage([FromBody] ProductImage image)
    {
        _context.ProductImages.Add(image);
        await _context.SaveChangesAsync();
        return Ok(image);
    }

    [HttpGet("{groupId}")]
    public async Task<ActionResult<IEnumerable<ProductImage>>> GetImages(int groupId)
    {
        return await _context.ProductImages
            .Where(img => img.PlanogramGroupId == groupId)
            .ToListAsync();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImage(int id)
    {
        var image = await _context.ProductImages.FindAsync(id);
        if (image == null) return NotFound();

        _context.ProductImages.Remove(image);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
