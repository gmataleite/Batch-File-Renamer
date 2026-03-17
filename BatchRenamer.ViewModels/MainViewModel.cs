using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BatchRenamer.Core;

namespace BatchRenamer.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private string _sourceFolderPath = string.Empty;
    public string SourceFolderPath
    {
        get => _sourceFolderPath;
        set
        {
            _sourceFolderPath = value;
            OnPropertyChanged();
        }
    }

    private string _destinationFolderPath = string.Empty;
    public string DestinationFolderPath
    {
        get => _destinationFolderPath;
        set
        {
            _destinationFolderPath = value;
            OnPropertyChanged();
        }
    }
    public string SearchText { get; set; } = string.Empty;
    public string ReplaceText { get; set; } = string.Empty;
    public bool CopyFiles { get; set; } = false;

    private string _resultMessage = string.Empty;
    public string ResultMessage
    {
        get => _resultMessage;
        set
        {
            _resultMessage = value;
            OnPropertyChanged(); 
        }
    }

    private readonly IFileService _fileService;

    public MainViewModel(IFileService fileService)
    {
        _fileService = fileService;
    }

    public void ExecuteRename()
    {
        try
        {
            var processor = new BatchRenamerProcessor(_fileService);
            
            int count = processor.Execute(SourceFolderPath, DestinationFolderPath, SearchText, ReplaceText, CopyFiles);
            
            ResultMessage = $"Renomeação concluída com sucesso. Arquivos alterados: {count}";
        }
        catch (ArgumentException ex)
        {
            ResultMessage = $"Erro: {ex.Message}";
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}