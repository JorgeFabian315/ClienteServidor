using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PruebaJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaludosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hola Mundo");
        }
    }
}
