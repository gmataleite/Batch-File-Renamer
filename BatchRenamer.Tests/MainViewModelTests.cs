using Xunit;
using BatchRenamer.ViewModels;

namespace BatchRenamer.Tests;

public class MainViewModelTests
{
    [Fact]
    public void ExecuteRename_WithInvalidSearchText_ShouldUpdateResultMessageWithError()
    {
        // Arrange
        var fakeService = new FakeFileService();
        fakeService.FilesToReturn = [ @"C:\temp\peca01.ipt" ];

        var viewModel = new MainViewModel(fakeService);

        viewModel.SourceFolderPath = @"C:\temp";
        viewModel.DestinationFolderPath = @"C:\temp";
        viewModel.SearchText = "/";
        viewModel.ReplaceText = "novo";

        // Act
        viewModel.ExecuteRename();

        // Assert
        Assert.Contains("inválidos", viewModel.ResultMessage);
        Assert.Equal("Red", viewModel.ResultMessageColor);
    }

    [Fact]
    public void ExecuteRename_WithValidInputs_ShouldUpdateResultMessageWithSuccess()
    {
        // Arrange
        var fakeService = new FakeFileService();
        fakeService.FilesToReturn = [ @"C:\temp\peca01.ipt" ];

        var viewModel = new MainViewModel(fakeService);

        viewModel.SourceFolderPath = @"C:\temp";
        viewModel.DestinationFolderPath = @"C:\temp";
        viewModel.SearchText = "peca";
        viewModel.ReplaceText = "part";

        // Act
        viewModel.ExecuteRename();

        // Assert
        Assert.Contains("sucesso", viewModel.ResultMessage);
        Assert.Contains("1", viewModel.ResultMessage);
        Assert.Equal("Green", viewModel.ResultMessageColor);
    }

    [Fact]
    public void ExecuteRename_WithNoMatchingFiles_ShouldShowRedMessage()
    {
        // Arrange
        var fakeService = new FakeFileService();
        fakeService.FilesToReturn = [];

        var viewModel = new MainViewModel(fakeService);

        viewModel.SourceFolderPath = @"C:\temp";
        viewModel.DestinationFolderPath = @"C:\temp";
        viewModel.SearchText = "peca";
        viewModel.ReplaceText = "part";

        // Act
        viewModel.ExecuteRename();

        // Assert
        Assert.Equal("Nenhum arquivo foi alterado.", viewModel.ResultMessage);
        Assert.Equal("Red", viewModel.ResultMessageColor);
    }

    [Fact]
    public void ExecuteRenameToDifferentFolder_WithValidInputs_ShouldUpdateResultMessageWithSuccess()
    {
        // Arrange
        var fakeService = new FakeFileService();
        fakeService.FilesToReturn = [ @"C:\temp\peca01.ipt" ];

        var viewModel = new MainViewModel(fakeService);

        viewModel.SourceFolderPath = @"C:\temp";
        viewModel.DestinationFolderPath = @"C:\temporario";
        viewModel.SearchText = "peca";
        viewModel.ReplaceText = "part";

        // Act
        viewModel.ExecuteRename();

        // Assert
        Assert.Contains("sucesso", viewModel.ResultMessage);
        Assert.Contains("1", viewModel.ResultMessage);
    }
}