using System.Windows;
using BatchRenamer.Core;

namespace BatchRenamer.WPF;

public partial class ConflictDialog : Window
{
    public FileConflictAction SelectedAction { get; private set; }

    public ConflictDialog(string fileName)
    {
        InitializeComponent();
        MessageText.Text = $"O arquivo '{fileName}' já existe na pasta de destino.\n\nO que você gostaria de fazer?";
    }

    private void BtnReplace_Click(object sender, RoutedEventArgs e)
    {
        SelectedAction = FileConflictAction.Replace;
        DialogResult = true;
    }

    private void BtnKeepBoth_Click(object sender, RoutedEventArgs e)
    {
        SelectedAction = FileConflictAction.KeepBoth;
        DialogResult = true;
    }

    private void BtnSkip_Click(object sender, RoutedEventArgs e)
    {
        SelectedAction = FileConflictAction.Skip;
        DialogResult = true;
    }
}
