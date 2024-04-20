using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PruebaAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            string[] datos = new string[] { "Hola mundo", "Adios mundo" };
            return Ok(datos);
        }

        /*
            HEAD - PRUEBA DE ACCESO
            GET - REGRESA UN OBJETO
            POST - CREA UN OBJETO
            PUT- REMPLAZA UN OBJETO
            DELETE - BORRA UN OBJETO
         */

        [HttpPost]
        public IActionResult Post(int numero)
        {
            //return Created("numero",numero * numero);
            return Ok(numero * numero);
        }


        [HttpGet("numero")]
        public IActionResult GetNumero()
        {
            return Ok(1000);
        }

        [HttpGet("{nombre}")]
        public IActionResult Saludar(string nombre)
        {
            return Ok("Hola " + nombre);
        }
    }
}
