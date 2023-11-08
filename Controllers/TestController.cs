using APIClient.Models;
using APIClient.Repository;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost, Authorize]
        public IActionResult LoginTeste([FromBody] LoginModel user)
        {
            return Ok();
        }
    }
}
