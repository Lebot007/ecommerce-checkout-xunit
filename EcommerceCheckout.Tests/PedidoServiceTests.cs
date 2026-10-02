using EcommerceCheckout.App;
using Xunit;

namespace EcommerceCheckout.Tests;

/// <summary>
/// Testes unitários da classe <see cref="PedidoService"/> escritos com xUnit,
/// utilizando o atributo [Fact] e as asserções Assert.Equal, Assert.True e Assert.False.
/// </summary>
public class PedidoServiceTests
{
    private readonly PedidoService _pedidoService = new PedidoService();

    // =====================================================================
    // TESTES EXIGIDOS PELA LISTA DE EXERCÍCIOS
    // =====================================================================

    /// <summary>
    /// Teste 1 (string): valida se GerarCodigoRastreio gera a máscara exata
    /// unindo a região em maiúsculas com o número do pedido em 4 dígitos.
    /// </summary>
    [Fact]
    public void GerarCodigoRastreio_DeveRetornarMascaraExata()
    {
        // Arrange
        string regiao = "sudeste";
        int numeroPedido = 42;

        // Act
        string resultado = _pedidoService.GerarCodigoRastreio(regiao, numeroPedido);

        // Assert
        Assert.Equal("SUDESTE-0042", resultado);
    }

    /// <summary>
    /// Teste 2 (int): valida se CalcularPontosFidelidade calcula os pontos
    /// corretamente (a cada R$ 10 em compras, o cliente ganha 2 pontos).
    /// </summary>
    [Fact]
    public void CalcularPontosFidelidade_DeveRetornar30PontosParaCompraDe150Reais()
    {
        // Arrange
        int valorTotal = 150;

        // Act
        int resultado = _pedidoService.CalcularPontosFidelidade(valorTotal);

        // Assert
        Assert.Equal(30, resultado);
    }

    /// <summary>
    /// Teste 3 (bool) - cenário VIP: cliente VIP com compra abaixo de R$ 200
    /// tem direito ao frete grátis (Assert.True).
    /// </summary>
    [Fact]
    public void TemDireitoAFreteGratis_ClienteVipAbaixoDe200_DeveRetornarTrue()
    {
        // Arrange
        int valorTotal = 150;
        bool eClienteVIP = true;

        // Act
        bool resultado = _pedidoService.TemDireitoAFreteGratis(valorTotal, eClienteVIP);

        // Assert
        Assert.True(resultado);
    }

    /// <summary>
    /// Teste 3 (bool) - cenário não VIP: cliente não VIP com compra abaixo de
    /// R$ 200 NÃO tem direito ao frete grátis (Assert.False).
    /// </summary>
    [Fact]
    public void TemDireitoAFreteGratis_ClienteNaoVipAbaixoDe200_DeveRetornarFalse()
    {
        // Arrange
        int valorTotal = 150;
        bool eClienteVIP = false;

        // Act
        bool resultado = _pedidoService.TemDireitoAFreteGratis(valorTotal, eClienteVIP);

        // Assert
        Assert.False(resultado);
    }

    // =====================================================================
    // TESTES ADICIONAIS DE COBERTURA (reforçam as regras de negócio)
    // =====================================================================

    /// <summary>
    /// Cobertura extra (string): outra região e outro número de pedido,
    /// garantindo que a máscara funcione para qualquer entrada.
    /// </summary>
    [Fact]
    public void GerarCodigoRastreio_OutraRegiao_DeveRetornarMascaraExata()
    {
        // Arrange & Act
        string resultado = _pedidoService.GerarCodigoRastreio("sul", 7);

        // Assert
        Assert.Equal("SUL-0007", resultado);
    }

    /// <summary>
    /// Cobertura extra (string): número de pedido com mais de 4 dígitos não
    /// deve ser truncado.
    /// </summary>
    [Fact]
    public void GerarCodigoRastreio_NumeroPedidoComMaisDe4Digitos_NaoDeveTruncar()
    {
        // Arrange & Act
        string resultado = _pedidoService.GerarCodigoRastreio("norte", 12345);

        // Assert
        Assert.Equal("NORTE-12345", resultado);
    }

    /// <summary>
    /// Cobertura extra (int): compra de R$ 0 não gera pontos.
    /// </summary>
    [Fact]
    public void CalcularPontosFidelidade_CompraZero_DeveRetornarZeroPontos()
    {
        // Arrange & Act
        int resultado = _pedidoService.CalcularPontosFidelidade(0);

        // Assert
        Assert.Equal(0, resultado);
    }

    /// <summary>
    /// Cobertura extra (int): valores que não fecham parcela completa de
    /// R$ 10 consideram apenas as parcelas inteiras (99 -> 9 * 2 = 18).
    /// </summary>
    [Fact]
    public void CalcularPontosFidelidade_ValorQuebrado_DeveConsiderarApenasParcelasInteiras()
    {
        // Arrange & Act
        int resultado = _pedidoService.CalcularPontosFidelidade(99);

        // Assert
        Assert.Equal(18, resultado);
    }

    /// <summary>
    /// Cobertura extra (bool): valor exatamente no limite (R$ 200) para
    /// cliente não VIP deve garantir frete grátis.
    /// </summary>
    [Fact]
    public void TemDireitoAFreteGratis_ValorNoLimite200NaoVip_DeveRetornarTrue()
    {
        // Arrange & Act
        bool resultado = _pedidoService.TemDireitoAFreteGratis(200, false);

        // Assert
        Assert.True(resultado);
    }

    /// <summary>
    /// Cobertura extra (bool): R$ 199 (um real abaixo do limite) para cliente
    /// não VIP NÃO deve garantir frete grátis.
    /// </summary>
    [Fact]
    public void TemDireitoAFreteGratis_Valor199NaoVip_DeveRetornarFalse()
    {
        // Arrange & Act
        bool resultado = _pedidoService.TemDireitoAFreteGratis(199, false);

        // Assert
        Assert.False(resultado);
    }

    /// <summary>
    /// Cobertura extra (bool): cliente VIP tem frete grátis mesmo com
    /// valor total zero.
    /// </summary>
    [Fact]
    public void TemDireitoAFreteGratis_VipComValorZero_DeveRetornarTrue()
    {
        // Arrange & Act
        bool resultado = _pedidoService.TemDireitoAFreteGratis(0, true);

        // Assert
        Assert.True(resultado);
    }
}
