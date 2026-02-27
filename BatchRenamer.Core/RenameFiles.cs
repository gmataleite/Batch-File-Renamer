using System.IO;
using System.Linq;

namespace BatchRenamer.Core;

public static class RenameFiles
{
    public static string GenerateNewName(string oldName, string searchedText, string replacedtext = "")
    {
        string oldNameWithoutExtension = Path.GetFileNameWithoutExtension(oldName);
        string extensionFile = Path.GetExtension(oldName);
        string newName;

        // Validação de caracteres inválidos
        char[] invalidChars = Path.GetInvalidFileNameChars();

        // Validação de busca vazia
        if (string.IsNullOrWhiteSpace(searchedText))
        {
            throw new ArgumentException("Campo \"Search\" não pode ficar em branco.");
        }

        // Verificamos se 'any' (algum) caractere da busca ou do replace é inválido
        if (searchedText.Any(c => invalidChars.Contains(c)) || replacedtext.Any(c => invalidChars.Contains(c)))
        {
            throw new ArgumentException("O texto contém caracteres inválidos para nomes de arquivos.");
        }

        newName = oldNameWithoutExtension.Replace(searchedText, replacedtext) + extensionFile;
        
        return newName;
    }
}
