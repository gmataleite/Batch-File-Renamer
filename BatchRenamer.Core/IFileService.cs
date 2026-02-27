namespace BatchRenamer.Core;

public interface IFileService
{
    string[] GetFilesFromDirectory(string path);
    void Move(string sourcePath, string destinationPath);
}