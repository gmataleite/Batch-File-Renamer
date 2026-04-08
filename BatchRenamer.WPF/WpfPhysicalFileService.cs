using System.IO;
using System.Windows;
using BatchRenamer.Core;

namespace BatchRenamer.WPF;

public class WpfPhysicalFileService : PhysicalFileService
{
    public override FileConflictAction AskUserConflictAction(string fileName)
    {
        var dialog = new ConflictDialog(fileName)
        {
            Owner = Application.Current.MainWindow
        };

        bool? result = dialog.ShowDialog();
        return result == true ? dialog.SelectedAction : FileConflictAction.Replace;
    }
}
