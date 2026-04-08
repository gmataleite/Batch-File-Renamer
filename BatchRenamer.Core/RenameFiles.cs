using System.IO;
using System.Linq;

namespace BatchRenamer.Core;

public static class RenameFiles
{
    public static string GenerateNewName(string oldFileName, string searchedText, string replacedtext = "", string destinationFolderPath = "")
    {
        string oldNameWithoutExtension = Path.GetFileNameWithoutExtension(oldFileName);
        string extensionFile = Path.GetExtension(oldFileName);
        string newName = "";

        char[] invalidChars = Path.GetInvalidFileNameChars();

        if (string.IsNullOrWhiteSpace(searchedText))
        {
            throw new ArgumentException("Campo \"Search\" não pode ficar em branco.");
        }

        if (searchedText.Any(c => invalidChars.Contains(c)) || replacedtext.Any(c => invalidChars.Contains(c)))
        {
            throw new ArgumentException("O texto contém caracteres inválidos para nomes de arquivos.");
        }

        return newName = oldNameWithoutExtension.Replace(searchedText, replacedtext) + extensionFile;
    }
}
