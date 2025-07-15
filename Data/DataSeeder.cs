using PlanogramBackend.Models;

namespace PlanogramBackend.Data
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.PlanogramGroups.Any())
            {
                var planogram = new List<PlanogramGroup>
                {
                    // Row 1 (Top Shelf)
                    new PlanogramGroup
                    {
                        Label = "Moisturizer",
                        ShelfRow = 1,
                        ProductImages = DuplicateImage("moist", 3)
                    },
                    new PlanogramGroup
                    {
                        Label = "Sunscreen",
                        ShelfRow = 1,
                        ProductImages = DuplicateImage("sun", 3)
                    },
                    new PlanogramGroup
                    {
                        Label = "Facewash",
                        ShelfRow = 1,
                        ProductImages = DuplicateImage("face", 3)
                    },

                    // Row 2 (Bottom Shelf)
                    new PlanogramGroup
                    {
                        Label = "Body Lotion",
                        ShelfRow = 2,
                        ProductImages = DuplicateImage("bodylotion", 3)
                    },
                    new PlanogramGroup
                    {
                        Label = "Shampoo",
                        ShelfRow = 2,
                        ProductImages = DuplicateImage("shampoo", 3)
                    },
                    new PlanogramGroup
                    {
                        Label = "Conditioner",
                        ShelfRow = 2,
                        ProductImages = DuplicateImage("conditioner", 2)
                    }
                };

                context.PlanogramGroups.AddRange(planogram);
                context.SaveChanges();
            }
        }

        // ✅ Updated: Automatically detects common image extensions
        private static List<ProductImage> DuplicateImage(string baseName, int count)
        {
            string[] extensions = [".jpg", ".jpeg", ".png", ".webp"]; // ✅ Add more if needed
            var foundFile = extensions.Select(ext => $"{baseName}{ext}")
                                      .FirstOrDefault(file => File.Exists(Path.Combine("wwwroot/images", file)));

            if (foundFile == null)
                throw new FileNotFoundException($"❌ Image file not found for: {baseName}");

            return Enumerable.Range(1, count)
                .Select(_ => new ProductImage { ImageUrl = foundFile })
                .ToList();
        }
    }
}
