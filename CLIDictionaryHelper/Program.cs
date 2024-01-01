using System.Text;
using CLIDictionaryHelper.LexicalData;

namespace CLIDictionaryHelper;

internal static class Program
{
    private static async Task Main()
    {
        while (true)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Please enter a word (or type '!q' to quit):");
            string? input = Console.ReadLine();
        
            if (string.IsNullOrEmpty(input)) continue;
            if (input.ToLower() == "!q") break;
        
            try
            {
                WordDefinition wordDefinition = await new CambridgeService().GetWordDefinition(input);
                await AnkiService.AddComplexCardToAnkiAsync(wordDefinition, "单词 CLI", "单词");
                Printer.Print(wordDefinition);
            } catch (Exception e)
            {
                Console.WriteLine("An Error occurred:");
                Console.WriteLine($"\t {e.Message}");
                Console.WriteLine($"\t {e.StackTrace}");
            }
        }

        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
    }
}