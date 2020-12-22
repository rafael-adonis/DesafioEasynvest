using Easynvest.Domain.Entities.Core;
using Easynvest.Domain.Entities.Validations;
using FluentValidation.Results;
using System;

namespace Easynvest.Domain.Entities
{
    public class RendaFixa : InvestimentoBase
    {
        private readonly ValidationResult validation = new ValidationResult();
        private readonly RendaFixaValidator rendaFixaValidator = new RendaFixaValidator();
        public RendaFixa(double aliquotaIR, 
                         int iof, 
                         string tipo, 
                         string nome, 
                         DateTime dataCompra, 
                         DateTime dataVencimento, 
                         double valorInvestido, 
                         double valorTotal,                         
                         double quantidade, 
                         double outrasTaxas,
                         double taxas, 
                         string indice,
                         bool guarantidoFGC, 
                         double precoUnitario, 
                         bool primario) 
            : base(aliquotaIR, iof, tipo, nome, dataCompra, dataVencimento, valorInvestido, valorTotal)
        {            
            Quantidade = quantidade;
            OutrasTaxas = outrasTaxas;
            Taxas = taxas;
            Indice = indice;
            GuarantidoFGC = guarantidoFGC;
            PrecoUnitario = precoUnitario;
            Primario = primario;
            rendaFixaValidator.Validate(this);
        }
        
        public double Quantidade { get; private set; }
        public double OutrasTaxas { get; private set; }
        public double Taxas { get; private set; }
        public string Indice { get; private set; }
        public bool GuarantidoFGC { get; private set; }
        public double PrecoUnitario { get; private set; }
        public bool Primario { get; private set; }
        public override bool IsValid => validation.Errors.Count == 0;

        public override ValidationResult ValidationResult => validation;
    }
}
