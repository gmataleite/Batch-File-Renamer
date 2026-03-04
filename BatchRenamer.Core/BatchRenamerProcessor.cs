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
            try
            {
                string fileName = Path.GetFileName(fileFullName);
                string newFileName = RenameFiles.GenerateNewName(fileName, search, replace);
                
                if (fileName != newFileName)
                {
                    string directory = string.IsNullOrWhiteSpace(destinationFolderPath) ? Path.GetDirectoryName(fileFullName)! : destinationFolderPath;
                    string destinationPath = Path.Combine(directory, newFileName);

                    if (copyFiles)
                    {
                        _fileService.Copy(fileFullName, destinationPath);
                    }
                    else
                    {
                        _fileService.Move(fileFullName, destinationPath);
                    }
                
                    renamedCount++;
                }
            }
            catch (IOException)
            {
                continue;
            }
        }

        return renamedCount;
    }
}