using System.IO;

namespace BatchRenamer.Core;

public class BatchRenamerProcessor
{
    private readonly IFileService _fileService;

    public BatchRenamerProcessor(IFileService fileService)
    {
        _fileService = fileService;
    }

    public int Execute(string sourcePath, string destinationFolderPath, string search, string replace, bool copyFiles)
    {
        string[] files = _fileService.GetFilesFromDirectory(sourcePath);
        int renamedCount = 0;

        foreach (var fileFullName in files)
        {
            string oldFileName = Path.GetFileName(fileFullName);
            string newFileName = RenameFiles.GenerateNewName(oldFileName, search, replace);

            string directory = string.IsNullOrWhiteSpace(destinationFolderPath)
                               ? Path.GetDirectoryName(fileFullName)!
                               : destinationFolderPath;

            string destinationPath = Path.Combine(directory, newFileName);

            // VERIFICAÇÃO DE CONFLITO AQUI
            if (_fileService.FileExists(destinationPath))
            {
                var action = _fileService.AskUserConflictAction(newFileName);

                if (action == FileConflictAction.Skip) continue;

                if (action == FileConflictAction.KeepBoth)
                {
                    destinationPath = GenerateUniquePath(destinationPath);
                }
            }

            try
            {
                if (copyFiles) _fileService.Copy(fileFullName, destinationPath);
                else _fileService.Move(fileFullName, destinationPath);

                renamedCount++;
            }
            catch (IOException)
            {
            }
        }
        return renamedCount;
    }

    private string GenerateUniquePath(string path)
    {
        int count = 1;
        string dir = Path.GetDirectoryName(path)!;
        string name = Path.GetFileNameWithoutExtension(path);
        string ext = Path.GetExtension(path);
        string newPath = path;

        while (_fileService.FileExists(newPath))
        {
            newPath = Path.Combine(dir, $"{name} ({count}){ext}");
            count++;
        }
        return newPath;
    }
}