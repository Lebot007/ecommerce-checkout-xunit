# ecommerce-checkout-xunit

![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)
![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4.svg)
![xUnit](https://img.shields.io/badge/testes-xUnit-009E66.svg)

> **Garantia da Qualidade de Software — Gestão e Qualidade de Software**
> Professor: Daniel Henrique Matos de Paiva
> Aluno: **João Vitor Alves Rodrigues** — RA: **32513480**

Solução **.NET 10** criada via terminal (.NET CLI) para gerenciar o cálculo de **cupons, itens e frete** de uma loja online. A classe `PedidoService` implementa métodos com retornos dos tipos **`string`**, **`int`** e **`bool`**, todos cobertos por **testes unitários no xUnit** utilizando `Assert.Equal`, `Assert.True` e `Assert.False`.

---

## 📁 Estrutura da solução

```
EcommerceCheckout.slnx                 -> Solução (criada com `dotnet new sln`)
├── EcommerceCheckout.App/             -> Código de produção (console)
│   ├── EcommerceCheckout.App.csproj
│   ├── Program.cs                     -> Demonstração de uso dos métodos
│   └── PedidoService.cs               -> Regras de negócio (string, int e bool)
└── EcommerceCheckout.Tests/           -> Testes unitários (xUnit)
    ├── EcommerceCheckout.Tests.csproj -> Referencia o projeto App
    └── PedidoServiceTests.cs          -> 11 testes com [Fact]
```

---

## 🧩 Métodos criados (`PedidoService`)

| # | Método (assinatura) | Regra de negócio | Exemplo |
|---|---------------------|------------------|---------|
| 1 | `string GerarCodigoRastreio(string regiao, int numeroPedido)` | Retorna a **região em MAIÚSCULAS** unida ao **número do pedido preenchido com zeros à esquerda (4 dígitos)**, no formato `REGIAO-0000`. | `("sudeste", 42)` → `"SUDESTE-0042"` |
| 2 | `int CalcularPontosFidelidade(int valorTotal)` | **A cada R$ 10 em compras, o cliente ganha 2 pontos** de fidelidade (`(valorTotal / 10) * 2`). | `150` → `30` *(150 / 10 = 15 parcelas de R$ 10 → 15 × 2)* |
| 3 | `bool TemDireitoAFreteGratis(int valorTotal, bool eClienteVIP)` | Frete grátis se o valor total for **maior ou igual a R$ 200** **OU** se o comprador for **cliente VIP**. | `(150, true)` → `true` <br> `(150, false)` → `false` |

---

## ✅ Cobertura dos testes (`PedidoServiceTests`)

### Testes exigidos pela lista

| Teste `[Fact]` | Método testado | Asserção | Cenário validado |
|----------------|----------------|----------|------------------|
| `GerarCodigoRastreio_DeveRetornarMascaraExata` | `GerarCodigoRastreio` (string) | `Assert.Equal("SUDESTE-0042", resultado)` | Máscara exata: região maiúscula + número com 4 dígitos |
| `CalcularPontosFidelidade_DeveRetornar30PontosParaCompraDe150Reais` | `CalcularPontosFidelidade` (int) | `Assert.Equal(30, resultado)` | 2 pontos a cada R$ 10 (compra de R$ 150 → 30 pontos) |
| `TemDireitoAFreteGratis_ClienteVipAbaixoDe200_DeveRetornarTrue` | `TemDireitoAFreteGratis` (bool) | `Assert.True(...)` | Cliente **VIP** com compra de R$ 150 (abaixo de R$ 200) tem frete grátis |
| `TemDireitoAFreteGratis_ClienteNaoVipAbaixoDe200_DeveRetornarFalse` | `TemDireitoAFreteGratis` (bool) | `Assert.False(...)` | Cliente **não VIP** com compra de R$ 150 (abaixo de R$ 200) NÃO tem frete grátis |

### Testes adicionais de cobertura (reforçam as regras de negócio)

| Teste `[Fact]` | Método testado | Cenário validado |
|----------------|----------------|------------------|
| `GerarCodigoRastreio_OutraRegiao_DeveRetornarMascaraExata` | `GerarCodigoRastreio` | Outra região/número: `("sul", 7)` → `"SUL-0007"` |
| `GerarCodigoRastreio_NumeroPedidoComMaisDe4Digitos_NaoDeveTruncar` | `GerarCodigoRastreio` | Número > 4 dígitos não é truncado: `("norte", 12345)` → `"NORTE-12345"` |
| `CalcularPontosFidelidade_CompraZero_DeveRetornarZeroPontos` | `CalcularPontosFidelidade` | Compra de R$ 0 → 0 pontos |
| `CalcularPontosFidelidade_ValorQuebrado_DeveConsiderarApenasParcelasInteiras` | `CalcularPontosFidelidade` | `99` → `18` pontos (apenas parcelas inteiras de R$ 10) |
| `TemDireitoAFreteGratis_ValorNoLimite200NaoVip_DeveRetornarTrue` | `TemDireitoAFreteGratis` | Valor-limite: R$ 200 (não VIP) → `true` |
| `TemDireitoAFreteGratis_Valor199NaoVip_DeveRetornarFalse` | `TemDireitoAFreteGratis` | R$ 199 (não VIP) → `false` |
| `TemDireitoAFreteGratis_VipComValorZero_DeveRetornarTrue` | `TemDireitoAFreteGratis` | VIP com valor R$ 0 → `true` |

### Resultado da suíte e cobertura de código (coverlet)

```
Passed!  - Failed: 0, Passed: 11, Skipped: 0, Total: 11 - EcommerceCheckout.Tests.dll (net10.0)
```

| Classe | Cobertura de linhas | Cobertura de branches |
|--------|--------------------|-----------------------|
| **`PedidoService`** (código de produção testado) | **100%** | **100%** |

> A cobertura é gerada pelo **coverlet.collector** (já incluído no template xUnit) com o comando abaixo. O relatório `coverage.cobertura.xml` é gravado em `EcommerceCheckout.Tests/TestResults/`:
>
> ```bash
> dotnet test --collect:"XPlat Code Coverage"
> ```

---

## 🚀 Instruções de execução

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) instalado (verifique com `dotnet --version`).

### Clonar e testar

```bash
git clone https://github.com/SEU-USUARIO/ecommerce-checkout-xunit.git
cd ecommerce-checkout-xunit
dotnet test
```

Saída esperada:

```
Passed!  - Failed: 0, Passed: 11, Skipped: 0, Total: 11 - EcommerceCheckout.Tests.dll (net10.0)
```

### Executar a demonstração (opcional)

```bash
dotnet run --project EcommerceCheckout.App
```

Saída esperada:

```
Código de rastreio: SUDESTE-0042
Pontos de fidelidade para R$ 150,00: 30 pontos
Frete grátis (R$ 150,00, cliente VIP): True
Frete grátis (R$ 150,00, cliente comum): False
```

---

## 🛠️ Recriando o projeto do zero (.NET CLI)

A solução foi criada inteiramente pelo terminal, com os comandos abaixo:

```bash
# 1. Cria a Solução
dotnet new sln -n EcommerceCheckout

# 2. Cria o projeto da aplicação (Código de Produção)
dotnet new console -n EcommerceCheckout.App -f net10.0

# 3. Cria o projeto de Testes Unitários com xUnit
dotnet new xunit -n EcommerceCheckout.Tests -f net10.0

# 4. Adiciona ambos os projetos à Solução
dotnet sln add EcommerceCheckout.App/EcommerceCheckout.App.csproj
dotnet sln add EcommerceCheckout.Tests/EcommerceCheckout.Tests.csproj

# 5. Adiciona a referência do projeto de Produção no projeto de Testes
dotnet add EcommerceCheckout.Tests/EcommerceCheckout.Tests.csproj reference EcommerceCheckout.App/EcommerceCheckout.App.csproj
```

Em seguida, foram implementados:

- `EcommerceCheckout.App/PedidoService.cs` — os três métodos com retornos `string`, `int` e `bool`;
- `EcommerceCheckout.Tests/PedidoServiceTests.cs` — os testes com `[Fact]`, `Assert.Equal`, `Assert.True` e `Assert.False` (o arquivo `UnitTest1.cs` gerado pelo template foi removido).

---

## 📄 Arquivos do repositório

| Arquivo | Descrição |
|---------|-----------|
| `README.md` | Documentação dos métodos, cobertura dos testes e instruções de execução |
| `.gitignore` | Arquivo oficial para C#/.NET (gerado com `dotnet new gitignore`) |
| `LICENSE` | Licença pública **MIT** |

---

## 📜 Licença

Este projeto está licenciado sob a [Licença MIT](LICENSE).
