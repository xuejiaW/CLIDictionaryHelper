namespace CLIDictionaryHelper;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        while (true) // 永久循环直到用户决定退出
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Please enter a word (or type '!q' to quit):");
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input)) continue;
            if (input.ToLower() == "!q") break;

            try
            {
                WordDefinition wordDefinition = await new CambridgeService().GetWordDefinition(input);
                Printer.Print(wordDefinition);
            } catch (Exception e)
            {
                Console.WriteLine("An Error occurred:");
                Console.WriteLine($"\t {e.Message}");
                Console.WriteLine($"\t {e.StackTrace}");
            }
        }
    }
}