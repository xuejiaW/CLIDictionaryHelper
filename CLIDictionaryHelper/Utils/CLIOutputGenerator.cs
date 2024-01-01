using CLIDictionaryHelper.LexicalData;

namespace CLIDictionaryHelper.Utils;

public static class CLIOutputGenerator
{
    private const string k_Bold = "\u001b[1m";
    private const string k_Red = "\u001b[31m";
    private const string k_Yellow = "\u001b[33m";
    private const string k_Reset = "\u001b[0m";


    public static IEnumerable<string> From(Definition word)
    {
        var lines = new List<string>();
        if (word.pronunciations.Count >= 2)
            lines.Add($"UK: {word.pronunciations[0].phonetic}, US: {word.pronunciations[1].phonetic}");

        word.definitions.ForEach(definition =>
        {
            lines.Add("------------------------------------");
            lines.Add($"{definition.word}");
            if (!string.IsNullOrEmpty(definition.partOfSpeech)) lines.Add($"<{definition.partOfSpeech}>");
            lines.Add($"{k_Yellow}Definition{k_Reset}:");
            lines.Add($"\t {definition.explanation.originText}");
            lines.Add($"\t {k_Bold}{definition.explanation.translatedText}{k_Reset}");
            lines.Add("");
            if (definition.examples.Count != 0) lines.Add($"{k_Yellow}Examples{k_Reset}:");
            definition.examples.ForEach(example =>
            {
                string exampleText = example.originText.Replace(definition.word, $"{k_Red}{definition.word}{k_Reset}");
                string translatedText
                    = example.translatedText.Replace(definition.word, $"{k_Red}{definition.word}{k_Reset}");

                lines.Add($"\t {exampleText}");
                lines.Add($"\t {translatedText}");
                lines.Add("");
            });
        });
        return lines;
    }
}