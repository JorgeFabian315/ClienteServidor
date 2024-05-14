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
            var context = HttpContext;

            if (User.Identity != null)
            {
                return Ok("Hola" + User.Identity.Name);
            }

            return Ok("Hola");

        }
    }
}
