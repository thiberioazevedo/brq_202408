using AutoMapper;
using DDD.Application.AutoMapper;
using DDD.Application.Interfaces;
using DDD.Application.Services;
using Xunit;

namespace DDD.Application.UnitTests.Services
{
    public class CDBAppServiceTests
    {
        public ICDBAppService CDBAppService { get; }

        public CDBAppServiceTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DomainToViewModelMappingProfile>());
            config.AssertConfigurationIsValid();

            CDBAppService = new CDBAppService(new Mapper(config));
        }

        [Fact]
        public void NaoRecolheImpostoAntesDaUltimaParcela()
        {
            var cDBViewModel = this.CDBAppService.Calcular(100, 3);

            Assert.Equal(decimal.Zero, cDBViewModel.CBDAnterior.Imposto);
            Assert.Equal(decimal.Zero, cDBViewModel.CBDAnterior.TaxaImposto);
            Assert.Equal(decimal.Zero, cDBViewModel.CBDAnterior.CBDAnterior.Imposto);
            Assert.Equal(decimal.Zero, cDBViewModel.CBDAnterior.CBDAnterior.TaxaImposto);
        }

        [Fact]
        public void RegistrosEmCadeia()
        {
            var cDB = this.CDBAppService.Calcular(1, 3);

            Assert.NotNull(cDB.CBDAnterior);
            Assert.NotNull(cDB.CBDAnterior.CBDAnterior);
            Assert.Null(cDB.CBDAnterior.CBDAnterior.CBDAnterior);
        }

        [Fact]
        public void MesesEmSequencia()
        {
            var cDB = this.CDBAppService.Calcular(1, 3);

            Assert.Equal(3, cDB.Mes);
            Assert.Equal(2, cDB.CBDAnterior.Mes);
            Assert.Equal(1, cDB.CBDAnterior.CBDAnterior.Mes);
        }

        [Fact]
        public void SaldoSequenciado()
        {
            var cDB = this.CDBAppService.Calcular(1, 3);

            Assert.Equal((decimal)1.21, cDB.CBDAnterior.ValorFinal);
            Assert.Equal((decimal)1.21, cDB.ValorInicial);
            Assert.Equal((decimal)1.1, cDB.CBDAnterior.CBDAnterior.ValorFinal);
            Assert.Equal((decimal)1.1, cDB.CBDAnterior.ValorInicial);
        }

        [Fact]
        public void ValorFinal()
        {
            var cDB = this.CDBAppService.Calcular(100, 1);

            Assert.Equal((decimal)107.53, cDB.ValorFinal);
        }

        [Fact]
        public void Imposto()
        {
            var cDB = this.CDBAppService.Calcular(100, 1);
            Assert.Equal((decimal)2.19, cDB.Imposto);

            cDB = this.CDBAppService.Calcular(100, 2);
            Assert.Equal((decimal)2.4, cDB.Imposto);

            cDB = this.CDBAppService.Calcular(100, 3);
            Assert.Equal((decimal)2.63, cDB.Imposto);
        }

        [Fact]
        public void TaxaImposto()
        {
            var cDB = this.CDBAppService.Calcular(100, 5);
            Assert.Equal((decimal)0.225, cDB.TaxaImposto);

            cDB = this.CDBAppService.Calcular(100, 6);
            Assert.Equal((decimal)0.225, cDB.TaxaImposto);


            cDB = this.CDBAppService.Calcular(100, 7);
            Assert.Equal((decimal)0.2, cDB.TaxaImposto);

            cDB = this.CDBAppService.Calcular(100, 11);
            Assert.Equal((decimal)0.2, cDB.TaxaImposto);

            cDB = this.CDBAppService.Calcular(100, 12);
            Assert.Equal((decimal)0.2, cDB.TaxaImposto);


            cDB = this.CDBAppService.Calcular(100, 13);
            Assert.Equal((decimal)0.175, cDB.TaxaImposto);

            cDB = this.CDBAppService.Calcular(100, 23);
            Assert.Equal((decimal)0.175, cDB.TaxaImposto);

            cDB = this.CDBAppService.Calcular(100, 24);
            Assert.Equal((decimal)0.175, cDB.TaxaImposto);


            cDB = this.CDBAppService.Calcular(100, 25);
            Assert.Equal((decimal)0.15, cDB.TaxaImposto);
        }

        [Fact]
        public void Juros()
        {
            var cDB = this.CDBAppService.Calcular(100, 1);
            Assert.Equal((decimal)9.72, cDB.Juros);

            cDB = this.CDBAppService.Calcular(100, 2);
            Assert.Equal((decimal)10.66, cDB.Juros);

            cDB = this.CDBAppService.Calcular(100, 3);
            Assert.Equal((decimal)11.70, cDB.Juros);

            cDB = this.CDBAppService.Calcular(100, 4);
            Assert.Equal((decimal)12.84, cDB.Juros);
        }
    }
}
