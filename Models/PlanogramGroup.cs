using System.ComponentModel.DataAnnotations.Schema;

namespace PlanogramBackend.Models
{
    public class PlanogramGroup
    {
        public int Id { get; set; }

        public string Label { get; set; } = string.Empty;

        public int ShelfRow { get; set; } // ✅ NEW: For row-wise grouping (1 = top, 2 = bottom, etc.)

        public List<ProductImage> ProductImages { get; set; } = new();
    }
}
