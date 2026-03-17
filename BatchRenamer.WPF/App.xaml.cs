using System.Configuration;
using System.Data;
using System.Windows;
using BatchRenamer.Core;
using BatchRenamer.ViewModels;

namespace BatchRenamer.WPF;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // 1. Instanciamos o acesso real ao disco
        IFileService physicalFileService = new PhysicalFileService();

        // 2. Injetamos na ViewModel
        var viewModel = new MainViewModel(physicalFileService);

        // 3. Criamos a janela principal e "colamos" a ViewModel nela (DataContext)
        var mainWindow = new MainWindow
        {
            DataContext = viewModel
        };

        // 4. Mostramos a janela
        mainWindow.Show();
    }
}

