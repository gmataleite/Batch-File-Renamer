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
    public bool CopyFiles { get; set; } = false;
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
        fakeService.FilesToReturn = [ @"C:\temp\peca01.ipt", @"C:\temp\peca02.ipt" ];
        
        var processor = new BatchRenamerProcessor(fakeService);

        // Act
        int result = processor.Execute(@"C:\temp", "peca", "part", false);

        // Assert
        Assert.Equal(2, result);
        Assert.Contains(@"C:\temp\part01.ipt", fakeService.MovedFiles);
        Assert.Contains(@"C:\temp\part02.ipt", fakeService.MovedFiles);
    }

    [Fact]
    public void Execute_DoNotRenameFiles_WhenFileIsOpen()
    {
        // Arrange
        var fakeService = new FakeFileService();
        fakeService.FilesToReturn = [ @"C:\temp\peca01.ipt" ];
        fakeService.SimulateFileLocked = true;

        var processor = new BatchRenamerProcessor(fakeService);
        
        // Act
        int result = processor.Execute(@"C:\temp", "peca", "part", false);

        // Assert
        Assert.Equal(0, result);
        Assert.Empty(fakeService.MovedFiles);
    }

    [Fact]
    public void Execute_ShouldCopyFilesWithNewName_WhenValidPathsAreProvided()
    {
        // Arrange
        var fakeService = new FakeFileService();
        fakeService.FilesToReturn = [ @"C:\temp\peca01.ipt", @"C:\temp\peca02.ipt" ];
        fakeService.CopyFiles = true;
        
        var processor = new BatchRenamerProcessor(fakeService);

        // Act
        int result = processor.Execute(@"C:\temp", "peca", "part", true);

        // Assert
        Assert.Equal(2, result);
        Assert.Contains(@"C:\temp\peca01.ipt", fakeService.FilesToReturn);
        Assert.Contains(@"C:\temp\peca02.ipt", fakeService.FilesToReturn);
        Assert.Contains(@"C:\temp\part01.ipt", fakeService.MovedFiles);
        Assert.Contains(@"C:\temp\part02.ipt", fakeService.MovedFiles);
        Assert.DoesNotContain(@"C:\temp\peca01.ipt", fakeService.MovedFiles);
        Assert.DoesNotContain(@"C:\temp\peca02.ipt", fakeService.MovedFiles);
        Assert.DoesNotContain(@"C:\temp\part01.ipt", fakeService.FilesToReturn);
        Assert.DoesNotContain(@"C:\temp\part02.ipt", fakeService.FilesToReturn);
    }
}