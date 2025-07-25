﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlanogramBackend.Data;
using PlanogramBackend.Models;

namespace PlanogramBackend.Controllers
{
    [ApiController]
    [Route("api/shelf-submissions")]
    public class ActualShelfController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActualShelfController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitShelf([FromBody] ActualShelfSubmission submission)
        {
            var selectedUrls = JsonConvert.DeserializeObject<List<string>>(submission.SelectedImageUrls) ?? new();
            var expectedUrls = await _context.ProductImages.Select(p => p.ImageUrl).ToListAsync();

            int matchedCount = selectedUrls.Intersect(expectedUrls).Count();
            int totalCount = expectedUrls.Count;

            float complianceScore = totalCount > 0 ? (float)matchedCount / totalCount * 100f : 0;

            var checklist = new Dictionary<string, bool>
            {
                ["available"] = matchedCount == totalCount,
                ["shelf"] = complianceScore >= 75,
                ["sequence"] = complianceScore >= 75,
                ["quantity"] = complianceScore >= 75,
            };

            submission.ComplianceScore = complianceScore;
            submission.SubmittedAt = DateTime.UtcNow;
            _context.ActualShelfSubmissions.Add(submission);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Submitted successfully!",
                complianceScore,
                checklist,
                matched = matchedCount,
                total = totalCount,
                mismatches = selectedUrls.Except(expectedUrls).ToList()
            });
        }

        [HttpGet]
        public IActionResult GetSubmissions()
        {
            var data = _context.ActualShelfSubmissions
                .OrderByDescending(s => s.SubmittedAt)
                .Take(10)
                .ToList();

            return Ok(data);
        }
    }
}
