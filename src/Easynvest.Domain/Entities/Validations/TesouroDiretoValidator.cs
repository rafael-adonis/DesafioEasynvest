﻿using FluentValidation;
using System;

namespace Easynvest.Domain.Entities.Validations
{
    public class TesouroDiretoValidator : AbstractValidator<TesouroDireto>
    {
        public TesouroDiretoValidator()
        {

            RuleFor(x => x.Indice)
                .NotNull()
                .NotEmpty()
                .WithMessage("O investimento deve conter a informação de indice");

            RuleFor(x => x.AliquotaIR)
                .NotEqual(double.MinValue)
                .WithMessage("O investimento deve conter a alíquota de IR");

            RuleFor(x => x.Tipo)
                .NotNull()
                .NotEmpty()
                .WithMessage("O investimento deve conter a informação de Tipo");

            RuleFor(x => x.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage("O investimento deve conter a informação de Nome");

            RuleFor(x => x.DataCompra)
                .NotEqual(DateTime.MinValue)
                .WithMessage("O investimento deve contar a data de compra");

            RuleFor(x => x.DataVencimento)
                .NotEqual(DateTime.MinValue)
                .WithMessage("O investimento deve contar a data de vencimento");
        }
    }
}
