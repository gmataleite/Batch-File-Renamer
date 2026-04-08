using System.Windows;
using Microsoft.Win32;
using BatchRenamer.ViewModels;

namespace BatchRenamer.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnProcurarOrigem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog
            {
                Title = "Selecione a pasta de origem"
            };

            if (dialog.ShowDialog() == true)
            {
                if (DataContext is MainViewModel viewModel)
                {
                    viewModel.SourceFolderPath = dialog.FolderName;
                }
            }
        }

        private void BtnProcurarDestino_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog
            {
                Title = "Selecione a pasta de destino"
            };

            if (dialog.ShowDialog() == true)
            {
                if (DataContext is MainViewModel viewModel)
                {
                    viewModel.DestinationFolderPath = dialog.FolderName;
                }
            }
        }

        private void BtnProcessar_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.ExecuteRename();
            }
        }
    }
}