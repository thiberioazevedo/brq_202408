using System;
using DDD.Domain.Core.Models;

namespace DDD.Domain.Models
{
    public class CDB : EntityAudit
    {
        public int Mes { get; internal set; }
        public decimal ValorInicial { get; internal set; }
        public decimal ValorFinal { get; internal set; }
        public decimal Imposto { get; internal set; }
        public decimal TaxaImposto { get; internal set; }
        public decimal ValorBruto { get; internal set; }
        public decimal Juros { get; private set; }
        public CDB CBDAnterior { get; internal set; }

        public CDB(CDB cDBAnterior)
        {
            CBDAnterior = cDBAnterior;
            Mes = CBDAnterior.Mes + 1;
            ValorInicial = CBDAnterior.ValorFinal;
        }

        public CDB(decimal valorInicial)
        {
            Mes = 1;
            ValorInicial = valorInicial;
        }

        public void SetTaxas()
        {
            ValorBruto = Math.Round(ValorInicial * (1 + (decimal)0.09 * (decimal)1.08), 2);
            ValorFinal = ValorBruto;
            Juros = ValorBruto - ValorInicial;
            TaxaImposto = 0;
            Imposto = 0;
        }
        public void SetImposto()
        {
            TaxaImposto = GetTaxaImposto(Mes);
            Imposto = Math.Round((ValorBruto - ValorInicial) * TaxaImposto, 2);
            ValorFinal = ValorBruto - Imposto;
        }
        decimal GetTaxaImposto(int meses)
        {
            switch (meses)
            {
                case int n when n <= 6:
                    return (decimal)0.225;

                case int n when n <= 12:
                    return (decimal)0.20;

                case int n when n <= 24:
                    return (decimal)0.175;

                default:
                    return (decimal)0.15;
            }
        }
    }
}
