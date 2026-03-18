# Batch File Renamer (C# / WPF / .NET 9)

Uma ferramenta desktop nativa para Windows focada na renomeação e cópia de arquivos em lote. Desenvolvida com rigor técnico, priorizando **Arquitetura Limpa**, **SOLID** e **Testes Automatizados**.

Este projeto evoluiu de um script procedural para uma aplicação corporativa, demonstrando a separação estrita entre a interface gráfica e o domínio de negócio.


## 📥 Download e Instalação (Para Usuários)
Não é necessário instalar o .NET para rodar a versão estável:
1. Acesse a página de [Releases](https://github.com/gmataleite/Batch-File-Renamer/releases/tag/v1.0.0).
2. Baixe o arquivo .zip da versão mais recente.
3.Extraia e execute o arquivo BatchRenamer.exe.

Nota: Por ser um executável autossuficiente (Single-File), o primeiro início pode levar alguns segundos a mais enquanto o Windows valida o pacote.

## 🚀 Tecnologias e Padrões
* **Framework:** .NET 9.0
* **Interface Gráfica (UI):** WPF (Windows Presentation Foundation) com XAML.
* **Arquitetura:** Camadas Isoladas (Core, ViewModels, Tests, WPF) utilizando o padrão **MVVM** (Model-View-ViewModel).
* **Testes:** xUnit com Mocks Manuais (*Fake Services*).
* **Metodologia:** TDD (Test-Driven Development).

## 🧠 Engenharia e Lógica de Negócio (Core)
O motor da aplicação (`BatchRenamer.Core`) é completamente agnóstico em relação à interface visual.
* **Injeção de Dependência (DI):** As operações de sistema operacional são abstraídas pela interface `IFileService`, permitindo testes isolados de lógica sem corromper o disco físico.
* **Data Binding:** A comunicação entre o processador e a tela gráfica ocorre via `INotifyPropertyChanged`, garantindo que a UI seja apenas um reflexo do estado da aplicação.
* **Integração de SO:** Utilização de `Microsoft.Win32.OpenFolderDialog` para delegação de navegação de diretórios ao núcleo do Windows.

## 🧪 Cobertura de Testes Automatizados
A aplicação foi construída com a mentalidade *Test-First*. A suíte do xUnit valida:
- Preservação de extensões de arquivo complexas (ex: `.ipt`, `.iam`).
- Proteção contra caminhos nulos, strings vazias e caracteres inválidos.
- Prova matemática de rotas de execução entre Mover (Move) e Copiar (Copy) através de injeção de dublês de infraestrutura.

## 💻 Como Executar
O projeto pode ser compilado como um único arquivo executável autossuficiente (Single-File Deployment):
```bash
dotnet publish BatchRenamer.WPF/BatchRenamer.WPF.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true