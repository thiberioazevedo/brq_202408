namespace DDD.Application.ViewModels
{
    public class CDBViewModel    {
        public int Mes { get; internal set; }
        public decimal ValorInicial { get; internal set; }
        public decimal ValorFinal { get; internal set; }
        public decimal Imposto { get; internal set; }
        public decimal TaxaImposto { get; internal set; }
        public decimal ValorBruto { get; internal set; }
        public decimal Juros { get; private set; }
        public CDBViewModel CBDAnterior { get; internal set; }
    }
}
