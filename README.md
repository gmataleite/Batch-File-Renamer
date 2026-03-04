# Batch File Renamer (C# / .NET)

Uma ferramenta para renomeação de arquivos em lote, desenvolvida com foco estrito em Arquitetura Limpa, SOLID e Testes Automatizados.

Este projeto é uma refatoração profunda de um script original em Python, evoluindo para o ecossistema .NET. O objetivo principal é garantir máxima robustez na manipulação de arquivos, separação clara de responsabilidades (IoC) e resiliência contra falhas de sistema operacional (I/O).

## 🚀 Tecnologias e Padrões
* **Linguagem:** C# (.NET 9)
* **Arquitetura:** Camadas Isoladas (Core, ViewModels, Tests, ConsoleApp)
* **Padrões de Projeto:** MVVM (Model-View-ViewModel), Injeção de Dependência (DI) e Inversão de Controle (IoC).
* **Testes:** xUnit com Mocks Manuais (Padrão Humble Object e Fake Services).
* **Metodologia:** TDD (Test-Driven Development).

## 🧠 Arquitetura e Lógica de Negócio (Core)
O núcleo da aplicação (`BatchRenamer.Core`) foi desenvolvido de forma totalmente isolada da interface gráfica, garantindo que as regras de negócio sejam puras e altamente testáveis.

**Injeção de Dependência:** As operações físicas de disco são abstraídas pela interface `IFileService`. Isso permite que o motor de renomeação (`BatchRenamerProcessor`) seja testado em isolamento absoluto, sem risco de corromper dados reais.

Resiliência: O sistema é blindado contra entradas inválidas (caracteres proibidos pelo OS) e falhas de infraestrutura (como tentativas de mover arquivos bloqueados por outros processos), capturando `IOException` e `ArgumentException` graciosamente.

As operações de disco e manipulação de strings protegem a integridade dos dados, como a preservação estrita das extensões originais dos arquivos durante a renomeação.

## 🧪 Cobertura de Testes Automatizados
O projeto adota uma abordagem *Test-First*. A lógica é validada através de testes unitários parametrizados (`[Theory]`) e (`[Fact]`), cobrindo cenários críticos:
* **Manipulação Segura:** Substituição de strings garantindo a preservação estrita das extensões originais (inclusive em arquivos com múltiplos pontos no nome, ex: `peca.v1.final.ipt`).
* **Casos de Borda (Edge Cases):** Tratamento de buscas vazias e injeção de caracteres inválidos de SO (ex: `/`, `*`, `?`).
* **Mocks de I/O:** Uso de um `FakeFileService` em memória para simular o disco rígido, validar o estado interno do processador e simular arquivos bloqueados.
* **Validação da ViewModel:** Testes garantindo que a camada de tradução (MVVM) capture as exceções do Core e atualize o status da interface corretamente.

## 💻 Como executar os testes (CLI)
1. Executar a Aplicação (MVP via Console)
A versão atual possui uma interface via CLI (Command Line Interface) totalmente funcional.
   ```bash
   # Navegue até a pasta do projeto executável
   cd BatchRenamer.ConsoleApp

   # Execute o programa
   dotnet run

2. Executar os Testes Automatizados
Para rodar a suíte de testes que valida as regras de negócio sem tocar no disco físico:
   ```bash
   # Na raiz do repositório
   dotnet test
   
## 🗺️ Roadmap
[x] Conclusão do MVP (CLI com Injeção de Dependência e MVVM base).

[x] Adicionar funcionalidade de "Copiar" arquivos (além de mover).

[x] Suporte para caminhos de destino diferentes do diretório de origem.

[ ] Construção da Interface Gráfica (GUI) nativa para Windows (WPF/WinUI 3).

