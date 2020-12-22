using Easynvest.Domain.Entities.Core;
using Easynvest.Domain.Entities.Validations;
using FluentValidation.Results;
using System;

namespace Easynvest.Domain.Entities
{
    public class FundoInvestimento : InvestimentoBase
    {
        private readonly ValidationResult validation = new ValidationResult();
        private readonly FundoInvestimentoValidator fundoInvestimentoValidator = new FundoInvestimentoValidator();
        public FundoInvestimento(double aliquotaIR,
                                 int iof,
                                 string tipo,
                                 string nome,
                                 DateTime dataCompra,
                                 DateTime dataVencimento,
                                 double valorInvestido,
                                 double valorTotal,
                                 int quantidade,
                                 double totalTaxas)
            : base(aliquotaIR, iof, tipo, nome, dataCompra, dataVencimento, valorInvestido, valorTotal)
        {
            Quantidade = quantidade;
            TotalTaxas = totalTaxas;
            fundoInvestimentoValidator.Validate(this);
        }

        public int Quantidade { get; private set; }
        public double TotalTaxas { get; private set; }
        public override bool IsValid => validation.Errors.Count == 0;
        public override ValidationResult ValidationResult => validation;
    }
}
