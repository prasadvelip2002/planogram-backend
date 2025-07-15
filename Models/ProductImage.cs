using System.ComponentModel.DataAnnotations.Schema;

namespace PlanogramBackend.Models
{
    [Table("productimages")] // ✅ matches MySQL table
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public int PlanogramGroupId { get; set; }

        public PlanogramGroup PlanogramGroup { get; set; } = null!;
    }
}
