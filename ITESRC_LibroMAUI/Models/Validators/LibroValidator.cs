using FluentValidation;
using ITESRC_LibroMAUI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITESRC_LibroMAUI.Models.Validators
{
    public class LibroValidator:AbstractValidator<LibroDTO>
    {
        public LibroValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("El titulo no debe estar vacio.");
            RuleFor(x => x.Autor).NotEmpty().WithMessage("El autor no debe estar vacio.");
            RuleFor(x => x.Portada).NotEmpty().WithMessage("La imagen de portada no debe estar vacio.");
            RuleFor(x => x.Portada).Must(ValidarURL).WithMessage("Escriba una dirección URL de una imagen JPEG");
        }

        private bool ValidarURL(string url)
        {
            if (url == null)
                return false;
            return url.StartsWith("https://") && url.EndsWith(".jpg");
        }
    }
}
