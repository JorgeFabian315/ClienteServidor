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
                    FechaActualizacion = DateTime.UtcNow,
                    Portada = dto.Portada,
                    Autor = dto.Autor
                };

                repository.Insert(libro);
                return Ok();

            }

            return BadRequest(results.Errors.Select(x => x.ErrorMessage));
        }

        [HttpGet("{fecha?}/{hora?}/{minuto?}")]
        public IActionResult Get(DateTime? fecha, int hora = 0,int minuto = 0)
        {

            if (fecha != null)
            {
                DateTime date = new DateTime(fecha.Value.Year, fecha.Value.Month, fecha.Value.Day, hora, minuto, 0);
            }

            var libros = repository.GetAll()
                .Where(x => fecha == null || x.FechaActualizacion > fecha)
                .OrderBy(x => x.Titulo)
                .Select(x => new LibroDto
                {
                    Id = x.Id,
                    Titulo = x.Titulo,
                    Eliminado = x.Eliminado,
                    Autor = x.Autor,
                    Portada =x.Portada,
                    Fecha = x.FechaActualizacion
                });

            return Ok(libros);
        }

        [HttpPut]
        public IActionResult Put(LibroDto dto)
        {

            LibroValidator validator = new LibroValidator();
            var results = validator.Validate(dto);
            if (results.IsValid)
            {
                var entidadLibro = repository.Get(dto.Id ?? 0);

                if (entidadLibro == null || entidadLibro.Eliminado == true)
                {
                    return NotFound();
                }
                else
                {
                    entidadLibro.Autor = dto.Autor;
                    entidadLibro.Titulo = dto.Titulo;
                    entidadLibro.Portada = dto.Portada;
                    entidadLibro.FechaActualizacion = DateTime.UtcNow;

                    repository.Update(entidadLibro);

                    return Ok();
                }
            }

            return BadRequest(results.Errors.Select(x => x.ErrorMessage));
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {

            var entidad = repository.Get(id);


            if(entidad == null || entidad.Eliminado)
            {
                return NotFound();
            }

            entidad.Eliminado = true;
            entidad.FechaActualizacion = DateTime.UtcNow;
            repository.Update(entidad);

            return Ok();
        }


    }
}
