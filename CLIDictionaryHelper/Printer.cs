using System.Text;

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

    public static void Print(WordDefinition wordDefinition)
    {
        var lines = new List<string>();
        lines.Add("Word: " + wordDefinition.word);
        wordDefinition.definitions.ForEach(definition =>
        {
            lines.Add("------------------------------------");
            lines.Add($"{definition.query}");
            lines.Add($"<{definition.partOfSpeech}>");
            lines.Add("Definition:");
            lines.Add($"\t {definition.explanation.originText}");
            lines.Add($"\t {definition.explanation.translatedText}");
            if (definition.examples.Count != 0) lines.Add("Examples:");
            definition.examples.ForEach(example =>
            {
                lines.Add($"\t {example.originText}");
                lines.Add($"\t {example.translatedText}");
            });
        });

        Print(lines);
    }


    private static void Print(IEnumerable<string> lines)
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
    }
}