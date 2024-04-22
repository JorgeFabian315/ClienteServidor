using FluentValidation;
using ITESRC_LibrosAPI.Models.DTOs;
namespace ITESRC_LibrosAPI.Validators
{
    public class LibroValidator : AbstractValidator<LibroDto>
    {
        public LibroValidator()
        {
            RuleFor(libro => libro.Titulo).NotEmpty().WithMessage("El titulo no debe de estar vacío");
            RuleFor(libro => libro.Portada).NotEmpty().WithMessage("La portada no debe de estar vacío")
                .Must(ValidarURL).WithMessage("Formato incorrecto portada");
            RuleFor(libro => libro.Autor).NotEmpty().WithMessage("El autor no debe de estar vacío");
        }

        private bool ValidarURL(string url)
        {
            return url.StartsWith("https://") && url.EndsWith(".jpg");
        }


    }
}
