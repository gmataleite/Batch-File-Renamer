using Xunit;
using BatchRenamer.ViewModels;

namespace BatchRenamer.Tests;

public class MainViewModelTests
{
    [Fact]
    public void ExecuteRename_WithInvalidSearchText_ShouldUpdateResultMessageWithError()
    {
        // Arrange
        // 1. Criamos a peça falsa (Dublê)
        var fakeService = new FakeFileService();
        fakeService.FilesToReturn = [ @"C:\temp\peca01.ipt" ];

        var viewModel = new MainViewModel(fakeService);

        viewModel.FolderPath = @"C:\temp";
        viewModel.SearchText = "/"; // Caractere inválido intencional
        viewModel.ReplaceText = "novo";

        // Act
        viewModel.ExecuteRename();

        // Assert
        // Verificamos se a propriedade de mensagem da tela contém a palavra "inválidos"
        Assert.Contains("inválidos", viewModel.ResultMessage);
    }

    [Fact]
    public void ExecuteRename_WithValidInputs_ShouldUpdateResultMessageWithSuccess()
    {
        // Arrange
        // 1. Criamos a peça falsa (Dublê)
        var fakeService = new FakeFileService();
        fakeService.FilesToReturn = [ @"C:\temp\peca01.ipt" ];

        var viewModel = new MainViewModel(fakeService);

        viewModel.FolderPath = @"C:\temp";
        viewModel.SearchText = "peca";
        viewModel.ReplaceText = "part";

        // Act
        viewModel.ExecuteRename();

        // Assert
        // Verificamos se a propriedade de mensagem da tela contém a palavra "sucesso"
        Assert.Contains("sucesso", viewModel.ResultMessage);
        Assert.Contains("1", viewModel.ResultMessage);
    }
}