﻿using FluentValidation;
using IA_V2.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Infrastructure.Validators
{
    public class TextDTOValidator : AbstractValidator<TextDTO>
    {
        public TextDTOValidator()
        {
            RuleFor(t => t.Id)
                .GreaterThan(0)
                .WithMessage("El IdUser debe ser mayor que 0");
            RuleFor(t => t.Content)
                .NotEmpty().WithMessage("El contenido del texto es obligatorio.")
                .MinimumLength(5).WithMessage("El texto debe tener al menos 5 caracteres.");

            RuleFor(t => t.UserId)
                .GreaterThan(0).WithMessage("Debe asociar un usuario válido al texto.");
        }
    }
}
