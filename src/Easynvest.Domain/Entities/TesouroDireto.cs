using Easynvest.Domain.Entities.Core;
using Easynvest.Domain.Entities.Validations;
using FluentValidation.Results;
using System;

namespace Easynvest.Domain.Entities
{
    public class TesouroDireto : InvestimentoBase
    {
        private readonly ValidationResult validation = new ValidationResult();
        private readonly TesouroDiretoValidator tesouroDiretoValidator = new TesouroDiretoValidator();

        public TesouroDireto(string indice, 
                             double aliquotaIR, 
                             int iof, 
                             string tipo, 
                             string nome, 
                             DateTime dataCompra, 
                             DateTime dataVencimento, 
                             double valorInvestido, 
                             double valorTotal): base(aliquotaIR, iof, tipo, nome, dataCompra, dataVencimento, valorInvestido, valorTotal)
        {
            Indice = indice;
            tesouroDiretoValidator.Validate(this);
        }
                
        public string Indice { get; private set; }

        public override bool IsValid => validation.Errors.Count == 0;

        public override ValidationResult ValidationResult => validation;
    }
}
