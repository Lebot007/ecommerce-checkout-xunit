using EcommerceCheckout.App;

// ============================================================
// EcommerceCheckout.App - Demonstração rápida do PedidoService
// (O foco da solução são os testes unitários: execute "dotnet test")
// ============================================================

PedidoService pedidoService = new PedidoService();

// Exemplo 1 (retorno string): código de rastreio
string codigoRastreio = pedidoService.GerarCodigoRastreio("sudeste", 42);
Console.WriteLine($"Código de rastreio: {codigoRastreio}");

// Exemplo 2 (retorno int): pontos de fidelidade
int pontos = pedidoService.CalcularPontosFidelidade(150);
Console.WriteLine($"Pontos de fidelidade para R$ 150,00: {pontos} pontos");

// Exemplo 3 (retorno bool): frete grátis
bool freteGratisVip = pedidoService.TemDireitoAFreteGratis(150, true);
bool freteGratisComum = pedidoService.TemDireitoAFreteGratis(150, false);
Console.WriteLine($"Frete grátis (R$ 150,00, cliente VIP): {freteGratisVip}");
Console.WriteLine($"Frete grátis (R$ 150,00, cliente comum): {freteGratisComum}");
