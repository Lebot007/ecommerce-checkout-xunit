namespace EcommerceCheckout.App;

/// <summary>
/// Serviço responsável pelas regras de negócio do checkout da loja online:
/// geração do código de rastreio, cálculo dos pontos de fidelidade e
/// validação do direito ao frete grátis.
/// </summary>
public class PedidoService
{
    /// <summary>
    /// (Retorno string) Gera o código de rastreio do pedido unindo a região
    /// em MAIÚSCULAS com o número do pedido preenchido com zeros à esquerda
    /// (4 dígitos), no formato "REGIAO-0000".
    /// </summary>
    /// <param name="regiao">Região de entrega (ex.: "sudeste").</param>
    /// <param name="numeroPedido">Número sequencial do pedido (ex.: 42).</param>
    /// <returns>
    /// Código de rastreio formatado.
    /// Exemplo: ("sudeste", 42) retorna "SUDESTE-0042".
    /// </returns>
    public string GerarCodigoRastreio(string regiao, int numeroPedido)
    {
        return $"{regiao.ToUpperInvariant()}-{numeroPedido:D4}";
    }

    /// <summary>
    /// (Retorno int) Calcula os pontos de fidelidade do cliente.
    /// Regra: a cada R$ 10 em compras, o cliente ganha 2 pontos.
    /// </summary>
    /// <param name="valorTotal">Valor total da compra em reais.</param>
    /// <returns>
    /// Quantidade de pontos acumulados.
    /// Exemplo: 150 retorna 30 (150 / 10 = 15 parcelas de R$ 10 -> 15 * 2 = 30).
    /// </returns>
    public int CalcularPontosFidelidade(int valorTotal)
    {
        return (valorTotal / 10) * 2;
    }

    /// <summary>
    /// (Retorno bool) Verifica se o cliente tem direito ao frete grátis.
    /// Regra: o frete é grátis se o valor total for maior ou igual a R$ 200
    /// OU se o comprador for um cliente VIP.
    /// </summary>
    /// <param name="valorTotal">Valor total da compra em reais.</param>
    /// <param name="eClienteVIP">Indica se o comprador é cliente VIP.</param>
    /// <returns>
    /// true quando há direito ao frete grátis; caso contrário, false.
    /// Exemplo: (150, true) retorna true; (150, false) retorna false.
    /// </returns>
    public bool TemDireitoAFreteGratis(int valorTotal, bool eClienteVIP)
    {
        return valorTotal >= 200 || eClienteVIP;
    }
}
