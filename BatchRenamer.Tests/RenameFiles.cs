using Xunit;
using BatchRenamer.Core;

namespace BatchRenamer.Tests;

public class RenameFilesTests
{
    [Theory]
    [InlineData("nome_original.extensao", "original", "novo", "nome_novo.extensao")]
    [InlineData("nome_original.original", "original", "novo", "nome_novo.original")]
    [InlineData("nome_original.extensao", "original", "", "nome_.extensao")]
    [InlineData("nome_original.extensao", "", "novo", "nome_original.extensao")]

    public void GenerateNewName_WhenReplacingText_ReturnsCorrectName(string originalName, string search, string replace, string expectedName)
    {
        // Arrange (Cenario)

        // Act (Ação)
        string changedName = RenameFiles.GenerateNewName(originalName, search, replace);

        // Assert (Verificação)
        Assert.Equal(expectedName, changedName);
    }

}
