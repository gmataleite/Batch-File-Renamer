using System;
using BatchRenamer.Core;

namespace BatchRenamer.ViewModels;

public class MainViewModel
{
    public string FolderPath { get; set; } = string.Empty;
    public string SearchText { get; set; } = string.Empty;
    public string ReplaceText { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;

    private readonly IFileService _fileService;

    public MainViewModel(IFileService fileService)
    {
        _fileService = fileService;
    }

    public void ExecuteRename()
    {
        try
        {
            // Instanciamos o Maestro usando a interface que a ViewModel recebeu
            var processor = new BatchRenamerProcessor(_fileService);
            
            // Lemos das próprias propriedades da classe
            int count = processor.Execute(FolderPath, SearchText, ReplaceText);
            
            // 3. Atualizamos a propriedade da tela em vez de usar 'return'
            ResultMessage = $"Renomeação concluída com sucesso. Arquivos alterados: {count}";
        }
        catch (ArgumentException ex)
        {
            // Usamos 'ex.Message' para pegar o texto exato do erro que vem lá do Core
            // Isso garante que o teste ache a palavra "inválidos"
            ResultMessage = $"Erro: {ex.Message}";
        }
    }
}