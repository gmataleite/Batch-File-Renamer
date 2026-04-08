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

        IFileService physicalFileService = new WpfPhysicalFileService();

        var viewModel = new MainViewModel(physicalFileService);

        var mainWindow = new MainWindow
        {
            DataContext = viewModel
        };

        mainWindow.Show();
    }
}

