using System.ComponentModel.DataAnnotations.Schema;

namespace PlanogramBackend.Models
{
    [Table("planogramgroups")] // ✅ matches MySQL table
    public class PlanogramGroup
    {
        public int Id { get; set; }

        public string Label { get; set; } = string.Empty;

        public int ShelfRow { get; set; }

        public List<ProductImage> ProductImages { get; set; } = new();
    }
}
