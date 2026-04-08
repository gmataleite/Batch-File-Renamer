using System.IO;

namespace BatchRenamer.Core;

public class PhysicalFileService : IFileService
{
    public string[] GetFilesFromDirectory(string path)
    {
        // Retorna todos os arquivos físicos do caminho especificado
        return Directory.GetFiles(path);
    }

    public void Move(string sourcePath, string destinationPath)
    {
        // Executa a movimentação/renomeação física no Windows
        File.Move(sourcePath, destinationPath);
    }
    
    public void Copy(string sourcePath, string destinationPath)
    {
        // Executa a movimentação/renomeação física no Windows
        File.Copy(sourcePath, destinationPath);
    }

    public bool FileExists(string path)
    {
        return File.Exists(path);
    }

    public virtual FileConflictAction AskUserConflictAction(string fileName)
    {
        // Para console, por padrão substituir
        return FileConflictAction.Replace;
    }
}