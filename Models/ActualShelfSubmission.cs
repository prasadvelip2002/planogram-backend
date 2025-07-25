﻿using System.ComponentModel.DataAnnotations.Schema;

namespace PlanogramBackend.Models
{
    [Table("actualshelfsubmissions")]  // 👈 matches the lowercase table in MySQL
    public class ActualShelfSubmission
    {
        public int Id { get; set; }
        public string SelectedImageUrls { get; set; } = string.Empty;
        public string PhotoBase64 { get; set; } = string.Empty;
        public float ComplianceScore { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
