# Batch File Renamer (C# / .NET)

Uma ferramenta de desktop para renomeação de arquivos em lote, desenvolvida com foco em arquitetura limpa e testes automatizados. 

Este projeto é uma refatoração de um script original em Python (Tkinter), evoluindo para o ecossistema .NET para garantir maior robustez, separação de responsabilidades e uma interface de usuário moderna.

## 🚀 Tecnologias e Padrões
* **Linguagem:** C# (.NET 8/9)
* **Arquitetura:** MVVM (Model-View-ViewModel) *[Em desenvolvimento]*
* **Testes:** xUnit
* **Metodologia:** TDD (Test-Driven Development)

## 🧠 Lógica de Negócio (Core)
O núcleo da aplicação (`BatchRenamer.Core`) foi desenvolvido de forma totalmente isolada da interface gráfica, garantindo que as regras de negócio sejam puras e altamente testáveis. 

As operações de disco e manipulação de strings protegem a integridade dos dados, como a preservação estrita das extensões originais dos arquivos durante a renomeação.

## 🧪 Testes Automatizados
O projeto adota uma abordagem *Test-First*. A lógica de renomeação é validada através de testes parametrizados (`[Theory]`), cobrindo cenários como:
- Substituição de strings simples.
- Proteção da extensão do arquivo quando o texto de busca for idêntico à extensão.
- Remoção de partes do nome através da injeção de strings vazias.

## 💻 Como executar os testes (CLI)
1. Clone este repositório.
2. Navegue até a pasta raiz do projeto.
3. Execute o comando:
   ```bash
   dotnet test
