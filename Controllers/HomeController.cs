using blog.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase

    {
        [HttpGet("")]
       // [ApiKey] caso necessite de conexão atraves de robô para realizar consultas - Chama o objeto ApiKeyAttribute
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
