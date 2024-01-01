using System.Text;
using CLIDictionaryHelper.LexicalData;

namespace CLIDictionaryHelper;

internal static class Program
{
    private static async Task Main()
    {
        // while (true) // 永久循环直到用户决定退出
        // {
        //     Console.WriteLine("-------------------------------------------");
        //     Console.WriteLine("Please enter a word (or type '!q' to quit):");
        //     string? input = Console.ReadLine();
        //
        //     if (string.IsNullOrEmpty(input)) continue;
        //     if (input.ToLower() == "!q") break;
        //
        //     try
        //     {
        //         WordDefinition wordDefinition = await new CambridgeService().GetWordDefinition(input);
        //         Printer.Print(wordDefinition);
        //     } catch (Exception e)
        //     {
        //         Console.WriteLine("An Error occurred:");
        //         Console.WriteLine($"\t {e.Message}");
        //         Console.WriteLine($"\t {e.StackTrace}");
        //     }
        // }

        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;


        // string[] modelNames = await AnkiSyncService.GetAnkiModelNamesAsync();
        // foreach (var modelName in modelNames)
        // {
        //     Console.WriteLine(modelName);
        // }

        // 假设您已经有了单词及其相关的各个字段的内容


        try
        {
            WordDefinition wordDefinition = await new CambridgeService().GetWordDefinition("wiggle");
            Printer.Print(wordDefinition);

            await AnkiService.AddComplexCardToAnkiAsync(wordDefinition, "单词 CLI", "单词");
        } catch (Exception e)
        {
            Console.WriteLine("An Error occurred:");
            Console.WriteLine($"\t {e.Message}");
            Console.WriteLine($"\t {e.StackTrace}");
        }
    }
}