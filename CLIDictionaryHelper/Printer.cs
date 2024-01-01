using System.Text;
using CLIDictionaryHelper.LexicalData;
using CLIDictionaryHelper.Utils;

namespace CLIDictionaryHelper;

public static class Printer
{
    private static bool s_IsInitialized = false;

    private static void Initialize()
    {
        s_IsInitialized = true;

        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
    }

    public static async Task Print(WordDefinition wordDefinition)
    {
        IEnumerable<string> lines = CLIOutputGenerator.From(wordDefinition);
        await Print(lines);
    }


    private static Task Print(IEnumerable<string> lines)
    {
        if (!s_IsInitialized) Initialize();

        var bufferLines = new List<string>();
        int windowHeight = Console.WindowHeight;

        foreach (string line in lines)
        {
            bufferLines.Add(line);
            if (bufferLines.Count != windowHeight - 1) continue;

            bufferLines.ForEach(Console.WriteLine);
            bufferLines.Clear();

            Console.ReadKey();
        }

        bufferLines.ForEach(Console.WriteLine);
        return Task.CompletedTask;
    }
}