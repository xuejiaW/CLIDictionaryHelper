using System.Text;
using HtmlAgilityPack;

namespace CLIDictionaryHelper;

internal static class Program
{
    private static string s_CambridgeUrl
        = "https://dictionary.cambridge.org/search/english-chinese-simplified/direct/?q=";

    private static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        var word = "wiggle"; // This would be dynamic based on user input.
        await GetWordDefinitions(word);
    }

    static async Task GetWordDefinitions(string word)
    {
        Console.WriteLine(word);
        string url = $"{s_CambridgeUrl}{word}";

        var client = new HttpClient();
        string response = await client.GetStringAsync(url);

        var doc = new HtmlDocument();
        doc.LoadHtml(response);

        string definitionsXPath = "//div[contains(@class, 'entry-body')]/*[contains(@class, 'entry-body__el')]";
        HtmlNodeCollection? entryNodes = doc.DocumentNode.SelectNodes(definitionsXPath);
        Console.WriteLine($"sss nodes count is {entryNodes.Count}");
        foreach (HtmlNode? entry in entryNodes)
        {
            string? partOfSpeech = entry.GetDivNoteText("pos dpos");
            var engDef = entry.GetDivNoteText("def ddef_d db");
            var znDef = entry.GetSpanNoteText("trans dtrans dtrans-se  break-cj");
            Console.WriteLine($"En Def : {engDef}");
            Console.WriteLine($"Zn Def : {znDef}");

            // Audio
            HtmlNodeCollection? definitionNodes = entry.SelectNodes(".//div[@class='def ddef_d db']");
            HtmlNodeCollection? exampleNodes = entry.SelectNodes(".//div[@class='examp dexamp']");

            Console.WriteLine($"Part of Speech: {partOfSpeech}");
        }
    }

    static string GetDivNoteText(this HtmlNode node, string className)
    {
        string? ret = node.SelectSingleNode($".//div[contains(@class, '{className}')]")?.InnerText?.Trim();
        return ret ?? "";
    }
    
    static string GetSpanNoteText(this HtmlNode node, string className)
    {
        string? ret = node.SelectSingleNode($".//span[@class='{className}']")?.InnerText?.Trim();
        return ret ?? "";
    }
}