using BatchRenamer.Core;
using BatchRenamer.ViewModels;

Console.WriteLine("================================");
Console.WriteLine("      BATCH FILE RENAMER        ");
Console.WriteLine("================================\n");

// 1. Instanciamos a infraestrutura física
IFileService physicalFileService = new PhysicalFileService();

// 2. Injetamos o serviço físico na ViewModel
var viewModel = new MainViewModel(physicalFileService);

// 3. Solicitamos os dados do usuário
Console.Write("Digite o caminho da pasta: ");
viewModel.FolderPath = Console.ReadLine() ?? string.Empty;

Console.Write("Digite o texto a ser buscado: ");
viewModel.SearchText = Console.ReadLine() ?? string.Empty;

Console.Write("Digite o texto de substituição (deixe em branco para remover): ");
viewModel.ReplaceText = Console.ReadLine() ?? string.Empty;

// 4. Dispara a execução da regra de negócio
Console.WriteLine("\nProcessando arquivos...");
viewModel.ExecuteRename();

// 5. Exibe o resultado que a ViewModel processou
Console.WriteLine("================================");
Console.WriteLine(viewModel.ResultMessage);
Console.WriteLine("================================\n");

Console.WriteLine("\nPressione qualquer tecla para encerrar...");
Console.ReadKey();