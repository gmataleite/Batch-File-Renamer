using Xunit;
using BatchRenamer.Core;
using System.Collections.Generic;

namespace BatchRenamer.Tests;

// Um "Dublê" para simular o sistema de arquivos
public class FakeFileService : IFileService
{
    public string[] FilesToReturn { get; set; } = [];
    public List<string> MovedFiles { get; } = new();
    public List<string> CopiedFiles { get; } = new();
    public bool SimulateFileLocked  { get; set; } = false;
    public HashSet<string> ExistingFiles { get; } = new();
    public FileConflictAction ConflictActionToReturn { get; set; } = FileConflictAction.Replace;
    
    public string[] GetFilesFromDirectory(string path) => FilesToReturn;

    public void Move(string sourcePath, string destinationPath)
    {
        if (SimulateFileLocked)
        {
            throw new IOException("O arquivo está sendo usado por outro processo.");
        }
        MovedFiles.Add(destinationPath);
    }

    public void Copy(string sourcePath, string destinationPath)
    {
        if (SimulateFileLocked)
        {
            throw new IOException("O arquivo está sendo usado por outro processo.");
        }
        CopiedFiles.Add(destinationPath);
    }

    public bool FileExists(string path) => ExistingFiles.Contains(path);

    public FileConflictAction AskUserConflictAction(string fileName) => ConflictActionToReturn;
}

public class BatchRenamerProcessorTests
{
    [Fact]
    public void Execute_ShouldRenameFiles_WhenValidPathsAreProvided()
    {
        // Arrange
        var fakeService = new FakeFileService();
        fakeService.FilesToReturn = [ @"c:/temp/peca01.ipt", @"c:/temp/peca02.ipt" ];
        
        var processor = new BatchRenamerProcessor(fakeService);

        // Act
        int result = processor.Execute(@"c:/temp", @"c:/temp", "peca", "part", false);

        // Assert
        Assert.Equal(2, result);
        Assert.Contains(@"c:/temp/part01.ipt", fakeService.MovedFiles);
        Assert.Contains(@"c:/temp/part02.ipt", fakeService.MovedFiles);
    }

    [Fact]
    public void Execute_DoNotRenameFiles_WhenFileIsOpen()
    {
        // Arrange
        var fakeService = new FakeFileService();
        fakeService.FilesToReturn = [ @"c:/temp/peca01.ipt" ];
        fakeService.SimulateFileLocked = true;

        var processor = new BatchRenamerProcessor(fakeService);
        
        // Act
        int result = processor.Execute(@"c:/temp", @"c:/temp", "peca", "part", false);

        // Assert
        Assert.Equal(0, result);
        Assert.Empty(fakeService.MovedFiles);
    }

    [Fact]
    public void Execute_ShouldCopyFilesWithNewName_WhenValidPathsAreProvided()
    {
        // Arrange
        var fakeService = new FakeFileService();
        fakeService.FilesToReturn = [ @"c:/temp/peca01.ipt", @"c:/temp/peca02.ipt" ];
        
        var processor = new BatchRenamerProcessor(fakeService);

        // Act
        int result = processor.Execute(@"c:/temp", @"c:/temp", "peca", "part", true);

        // Assert
        Assert.Equal(2, result);
        Assert.Contains(@"c:/temp/peca01.ipt", fakeService.FilesToReturn);
        Assert.Contains(@"c:/temp/peca02.ipt", fakeService.FilesToReturn);
        Assert.Contains(@"c:/temp/part01.ipt", fakeService.CopiedFiles);
        Assert.Contains(@"c:/temp/part02.ipt", fakeService.CopiedFiles);
        Assert.DoesNotContain(@"c:/temp/peca01.ipt", fakeService.CopiedFiles);
        Assert.DoesNotContain(@"c:/temp/peca02.ipt", fakeService.CopiedFiles);
        Assert.DoesNotContain(@"c:/temp/part01.ipt", fakeService.FilesToReturn);
        Assert.DoesNotContain(@"c:/temp/part02.ipt", fakeService.FilesToReturn);
    }

    [Fact]
    public void Execute_ShouldRenameAndCopyFiles_WhenValidPathsAreProvided()
    {
        // Arrange
        var fakeService = new FakeFileService();
        fakeService.FilesToReturn = [ @"c:/temp/peca01.ipt", @"c:/temp/peca02.ipt" ];
        
        var processor = new BatchRenamerProcessor(fakeService);

        // Act
        int result = processor.Execute(@"c:/temp", @"c:/temporario", "peca", "part", true);

        // Assert
        Assert.Equal(2, result);
        Assert.Contains(@"c:/temporario/part01.ipt", fakeService.CopiedFiles);
        Assert.Contains(@"c:/temporario/part02.ipt", fakeService.CopiedFiles);
    }

    [Fact]
    public void Execute_ShouldSkipFile_WhenConflictActionIsSkip()
    {
        // Arrange
        var fakeService = new FakeFileService();
        fakeService.FilesToReturn = [ @"c:/temp/peca01.ipt" ];
        fakeService.ExistingFiles.Add(@"c:/temp/part01.ipt");
        fakeService.ConflictActionToReturn = FileConflictAction.Skip;
        
        var processor = new BatchRenamerProcessor(fakeService);

        // Act
        int result = processor.Execute(@"c:/temp", @"c:/temp", "peca", "part", false);

        // Assert
        Assert.Equal(0, result);
        Assert.Empty(fakeService.MovedFiles);
    }

    [Fact]
    public void Execute_ShouldKeepBothFiles_WhenConflictActionIsKeepBoth()
    {
        // Arrange
        var fakeService = new FakeFileService();
        fakeService.FilesToReturn = [ @"c:/temp/peca01.ipt" ];
        fakeService.ExistingFiles.Add(@"c:/temp/part01.ipt");
        fakeService.ConflictActionToReturn = FileConflictAction.KeepBoth;
        
        var processor = new BatchRenamerProcessor(fakeService);

        // Act
        int result = processor.Execute(@"c:/temp", @"c:/temp", "peca", "part", false);

        // Assert
        Assert.Equal(1, result);
        Assert.Contains(@"c:/temp/part01 (1).ipt", fakeService.MovedFiles);
    }

    [Fact]
    public void Execute_ShouldReplaceFile_WhenConflictActionIsReplace()
    {
        // Arrange
        var fakeService = new FakeFileService();
        fakeService.FilesToReturn = [ @"c:/temp/peca01.ipt" ];
        fakeService.ExistingFiles.Add(@"c:/temp/part01.ipt");
        fakeService.ConflictActionToReturn = FileConflictAction.Replace;
        
        var processor = new BatchRenamerProcessor(fakeService);

        // Act
        int result = processor.Execute(@"c:/temp", @"c:/temp", "peca", "part", false);

        // Assert
        Assert.Equal(1, result);
        Assert.Contains(@"c:/temp/part01.ipt", fakeService.MovedFiles);
    }
}