# Batch File Renamer (C# / WPF / .NET 9)

A native Windows desktop tool focused on batch file renaming and copying. Developed with technical rigor, prioritizing **Clean Architecture**, **SOLID principles**, and **Automated Testing**.

This project evolved from a procedural script into an enterprise-grade application, demonstrating a strict separation between the Graphical User Interface (GUI) and the business domain.


## 📥 Download & Installation (For Users)
You don't need the .NET SDK installed to run the stable version:
1. Go to the [Releases](https://github.com/gmataleite/Batch-File-Renamer/releases/tag/v1.0.0) page.
2. Download the latest version .zip file.
3. Extract and run BatchRenamer.exe.

Note: As a self-contained (Single-File) executable, the first launch might take a few extra seconds while Windows validates the package.

## 🚀 Technologies and Patterns
* **Framework:** .NET 9.0
* **Graphical Interface (UI):** WPF (Windows Presentation Foundation) com XAML.
* **Architecture:** Isolated Layers (Core, ViewModels, Tests, WPF) utilizing the **MVVM** (Model-View-ViewModel) pattern.
* **Tests:** xUnit with Manual Mocks (*Fake Services*).
* **Methodology:** TDD (Test-Driven Development).

## 🧠 Engineering & Business Logic (Core)
The application engine (`BatchRenamer.Core`) is completely agnostic to the visual interface.
* **Dependency Injection (DI):** Operating system operations are abstracted via the IFileService interface, allowing isolated logic testing without affecting the physical disk.
* **Data Binding:** Communication between the processor and the UI occurs via `INotifyPropertyChanged`, ensuring the UI is strictly a reflection of the application state.
* **OS Integration:** Utilizes `Microsoft.Win32.OpenFolderDialog` to delegate directory navigation to the Windows shell.

## 🧪 Automated Test Coverage
The application was built with a *Test-First* mindset. The xUnit suite validates:
- Preservation of complex file extensions (ex: `.ipt`, `.iam`).
- Protection against null paths, empty strings, and invalid characters.
- Verification of execution paths for Move and Copy operations through infrastructure double injection.

## 💻 How to Run
The project can be compiled as a self-contained Single-File executable:
```bash
dotnet publish BatchRenamer.WPF/BatchRenamer.WPF.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true