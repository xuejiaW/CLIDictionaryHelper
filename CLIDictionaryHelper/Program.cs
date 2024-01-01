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
            // string? input = Console.ReadLine();

            string? input = " on the go";

            if (string.IsNullOrEmpty(input)) continue;
            if (input.ToLower() == "!q") break;

            try
            {
                // Whether input is word or phase
                bool isWord = input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Length == 1;
                Definition definition = isWord
                    ? await new CambridgeService().GetWordDefinition(input)
                    : await new CambridgeService().GetPhraseDefinition(input);
                Task ankiTask = AnkiService.AddComplexCardToAnkiAsync(definition, "单词 CLI", "单词");
                Task printTask = Printer.Print(definition);
                await Task.WhenAll(ankiTask, printTask);
            } catch (Exception e)
            {
                Console.WriteLine("An Error occurred:");
                Console.WriteLine($"\t {e.Message}");
                Console.WriteLine($"\t {e.StackTrace}");
            }

            break;
        }

        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
    }
}