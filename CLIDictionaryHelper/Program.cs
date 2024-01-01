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
                // Whether input is word or phase
                bool isWord = input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Length == 1;
                Definition? definition = isWord
                    ? await new CambridgeService().GetWordDefinition(input)
                    : await new CambridgeService().GetPhraseDefinition(input);
                if (definition == null)
                {
                    Console.WriteLine($"No information for {input}");
                    continue;
                }

                await Printer.Print(definition.Value);
                Console.WriteLine("Save to anki? 'y' for save, Others for not");
                string? save = Console.ReadLine();
                if (save == "y") await AnkiService.AddComplexCardToAnkiAsync(definition.Value, "单词 CLI", "单词");
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