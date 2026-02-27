namespace BatchRenamer.Core;

public class BatchRenamerProcessor
{
    private readonly IFileService _fileService;

    public BatchRenamerProcessor(IFileService fileService)
    {
        _fileService = fileService;
    }

    public int Execute(string folderPath, string search, string replace)
    {
        string[] files = _fileService.GetFilesFromDirectory(folderPath);
        int renamedCount = 0;

        foreach (var fileFullName in files)
        {
            try
            {
                string fileName = Path.GetFileName(fileFullName);
                string newFileName = RenameFiles.GenerateNewName(fileName, search, replace);
                
                if (fileName != newFileName)
                {
                    string directory = Path.GetDirectoryName(fileFullName)!;
                    string destinationPath = Path.Combine(directory, newFileName);

                    _fileService.Move(fileFullName, destinationPath);
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