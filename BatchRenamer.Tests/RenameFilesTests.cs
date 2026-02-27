using Xunit;
using BatchRenamer.Core;

namespace BatchRenamer.Tests;

public class RenameFilesTests
{
   
    [Theory]
    [InlineData("old_name.extension", "old", "new", "new_name.extension")]
    [InlineData("old_name.old", "old", "new", "new_name.old")]
    [InlineData("old_name.extension", "old", "", "_name.extension")]
    [InlineData("old_old_name.extension", "old", "new", "new_new_name.extension")]
    [InlineData("old.name.extension", "old", "new", "new.name.extension")]
    
    public void GenerateNewName_WhenReplacingText_ReturnsCorrectName(string oldName, string search, string replace, string expectedName)
    {
        // Arrange (Cenario)

        // Act (Ação)
        string changedName = RenameFiles.GenerateNewName(oldName, search, replace);

        // Assert (Verificação)
        Assert.Equal(expectedName, changedName);
    }

    // string.IsNullOrWhiteSpace(searchedText)

    [Theory]
    [InlineData("old_name.extension", "/", "new")]
    [InlineData("old_name.extension", "", "new")]
    [InlineData("old_name.extension", "old", "/")]

    public void GenerateNewName_WithInvalidCharacter_ReturnException (string oldName, string search, string replace)
    {
        // Arrange
        // string oldName = "old_name.extension";
        // string search = "/";
        // string replace = "new";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => RenameFiles.GenerateNewName(oldName, search, replace));

    }

}
