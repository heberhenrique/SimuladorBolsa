using System;
using FluentAssertions;
using Moq;
using SimuladorBolsa.Domain.ViewModel;

namespace SimuladorBolsa.Services.Tests
{
    public class SimuladorCompraServiceTests
    {
        public SimuladorCompraServiceTests()
        {

        }

        [Theory]
        [InlineData("2022-09-26 18:46:30", false)]
        [InlineData("2022-09-26 16:59:59", true)]
        [InlineData("2022-09-26 10:00:30", true)]
        public void TestarHoraioOrdemDeCompra(DateTime horaOrdem, bool expectedResult)
        {
            // Arrange
            Ordem ordem = new Ordem();
            //ordem.DataHoraOrdem = new DateTime(2022, 9, 23, 11, 30, 30);
            ordem.DataHoraOrdem = horaOrdem;
            var _service = new SimuladorCompraService();

            // Act
            var result = _service.ValidarHorarioOrdem(ordem);

            // Assert
            //Assert.Equal(result, expectedResult);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(100, true)]
        [InlineData(999, false)]
        [InlineData(1, false)]
        public void TestarQuantidadeDePapeisComNumerosMaioresQueZero(int quantidade, bool expectedResult)
        {
            // Arrange
            Ordem ordem = new Ordem();
            ordem.QuantidadePapeis = quantidade;
            var _service = new SimuladorCompraService();

            // Act
            var result = _service.ValidarQuantidadePapeisOrdem(ordem);

            // Assert
            //Assert.Equal(result, expectedResult);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(-100)]
        [InlineData(-999)]
        [InlineData(-1)]
        [InlineData(0)]
        public void TestarQuantidadeDePapeisComValorMenorOuIgualAZero(int quantidade)
        {
            // Arrange
            Ordem ordem = new Ordem();
            ordem.QuantidadePapeis = quantidade;
            var _service = new SimuladorCompraService();

            // Act / Assert
            //Assert.ThrowsAny<Exception>(() => _service.ValidarQuantidadePapeisOrdem(ordem));
            Action act = () => _service.ValidarQuantidadePapeisOrdem(ordem);
            act.Should().Throw<Exception>()
                .WithMessage("Valor da ordem não pode ser menor ou igual zero");
        }

        [Theory]
        [InlineData("VALE3", true)]
        [InlineData("SFRA3", false)]
        public async void TestarSeCodigoDaAcaoExiste(string codigoAcao, bool expectedResult)
        {
            // Arrange
            Ordem ordem = new Ordem();
            ordem.Ticker = codigoAcao;
            var _service = new SimuladorCompraService();

            // Act
            var result = await _service.ValidarTickerAcao(ordem);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("VALE3", true)]
        [InlineData("VIIA3", true)]
        [InlineData("SFRA3", false)]
        public async void TestarSeCodigoDaAcaoExisteMock(string codigoAcao, bool expectedResult)
        {
            // Arrange
            Ordem ordem = new Ordem();
            ordem.Ticker = codigoAcao;
            Mock<SimuladorCompraService> mock = new Mock<SimuladorCompraService>();
            List<Empresa> empresas = new List<Empresa>();
            var empresa = new Empresa();
            empresa.CdAcao = "VALE3";
            empresas.Add(empresa);

            mock.Setup(x => x.ObterListaAcoes()).ReturnsAsync(empresas);

            // Act
            var result = await mock.Object.ValidarTickerAcao(ordem);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public async void TestarChamadaApiB3()
        {
            // Arrange
            var _service = new SimuladorCompraService();

            // Act
            var result = await _service.ObterListaAcoes();

            // Assert
            result.Should().NotBeNullOrEmpty();
        }
    }
}

