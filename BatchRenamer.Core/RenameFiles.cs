using System.IO;

namespace BatchRenamer.Core;

public static class RenameFiles
{
    public static string GenerateNewName(string oldName, string searchedText, string replacedtext = "")
    {
        string oldNameWithoutExtension = Path.GetFileNameWithoutExtension(oldName);
        string extensionFile = Path.GetExtension(oldName);

        string newName;

        if (searchedText == "")
        {
            newName = oldName;
        }
        else {
            newName = oldNameWithoutExtension.Replace(searchedText, replacedtext) + extensionFile;
        }

        return newName;
    }
}
