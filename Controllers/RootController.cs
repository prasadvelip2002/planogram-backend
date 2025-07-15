using Microsoft.AspNetCore.Mvc;

namespace PlanogramBackend.Controllers
{
    [ApiController]
    [Route("/")]
    public class RootController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("✅ Welcome to Planogram Backend API!");
        }
    }
}
