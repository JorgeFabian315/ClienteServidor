using ITESRC_LibrosAPI.Models.DTOs;
using ITESRC_LibrosAPI.Models.Entities;
using ITESRC_LibrosAPI.Repositories;
using ITESRC_LibrosAPI.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITESRC_LibrosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly LibrosRepository repository;

        public LibrosController(LibrosRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public IActionResult Post(LibroDto dto)
        {
            //Validar
            LibroValidator validator = new LibroValidator();
            var results = validator.Validate(dto);
            if (results.IsValid)
            {
                var libro = new Libros()
                {
                    Id = 0,
                    Titulo = dto.Titulo,
                    Eliminado = false,
                    FechaActualizacion = DateTime.Now,
                    Portada = dto.Portada,
                    Autor = dto.Autor
                };

                repository.Insert(libro);
                return Ok();

            }

            return BadRequest(results.Errors.Select(x => x.ErrorMessage));
        }
    }
}
