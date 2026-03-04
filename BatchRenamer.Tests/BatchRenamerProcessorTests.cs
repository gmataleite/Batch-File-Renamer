using Xunit;
using BatchRenamer.Core;
using System.Collections.Generic;

namespace BatchRenamer.Tests;

// Um "Dublê" simples para simular o sistema de arquivos
public class FakeFileService : IFileService
{
    public string[] FilesToReturn { get; set; } = [];
    public List<string> MovedFiles { get; } = new();
    public bool SimulateFileLocked  { get; set; } = false;
    
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
        MovedFiles.Add(destinationPath);
    }
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
        Assert.Contains(@"c:/temp/part01.ipt", fakeService.MovedFiles);
        Assert.Contains(@"c:/temp/part02.ipt", fakeService.MovedFiles);
        Assert.DoesNotContain(@"c:/temp/peca01.ipt", fakeService.MovedFiles);
        Assert.DoesNotContain(@"c:/temp/peca02.ipt", fakeService.MovedFiles);
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
        Assert.Contains(@"c:/temporario/part01.ipt", fakeService.MovedFiles);
        Assert.Contains(@"c:/temporario/part02.ipt", fakeService.MovedFiles);
    }
}