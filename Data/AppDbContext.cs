using Microsoft.EntityFrameworkCore;
using PlanogramBackend.Models;

namespace PlanogramBackend.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<PlanogramGroup> PlanogramGroups { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ActualShelfSubmission> ActualShelfSubmissions { get; set; }
    }
}
