using Microsoft.AspNetCore.Mvc;

namespace APIClient.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
