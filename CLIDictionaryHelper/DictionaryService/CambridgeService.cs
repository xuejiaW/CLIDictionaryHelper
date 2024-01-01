using System.Diagnostics;
using CLIDictionaryHelper.LexicalData;
using HtmlAgilityPack;

namespace CLIDictionaryHelper;

public class CambridgeService : IDictionaryService
{
    private const string k_CambridgeQueryUrl = $"{k_CambridgeUrl}/search/english-chinese-simplified/direct/?q=";
    private const string k_CambridgeUrl = "https://dictionary.cambridge.org";

    public async Task<HtmlDocument> GetQueryResult(string query)
    {
        string response = await new HttpClient().GetStringAsync($"{k_CambridgeQueryUrl}{query}");

        var doc = new HtmlDocument();
        doc.LoadHtml(response);
        return doc;
    }

    public async Task<Definition?> GetWordDefinition(string word)
    {
        HtmlDocument doc = await GetQueryResult(word);

        var wordDefinition = new Definition();

        string definitionsXPath = "//div[contains(@class, 'entry-body')]/*[contains(@class, 'entry-body__el')]";
        string actualWord = doc.DocumentNode.GetSubSpanNodeText("hw dhw");
        if (string.IsNullOrEmpty(actualWord)) return null;
        HtmlNodeCollection? entries = doc.DocumentNode.SelectNodes(definitionsXPath);
        FillDefinitions(wordDefinition.definitions, entries, actualWord);

        HtmlNodeCollection phonetics = doc.DocumentNode.SelectNodes(".//span[contains(@class, 'pron dpron')]");
        HtmlNodeCollection audioUrls = doc.DocumentNode.SelectNodes(".//source[@type='audio/mpeg']");

        for (int i = 0; i < 2; i++)
        {
            string phonetic = phonetics[i].GetNodeText();
            string audioUrl = $"{k_CambridgeUrl}{audioUrls[i].GetAttributeValue("src", "")}";
            wordDefinition.pronunciations.Add(new Pronunciation(phonetic, audioUrl));
        }

        return wordDefinition;
    }

    public async Task<Definition?> GetPhraseDefinition(string phrase)
    {
        HtmlDocument doc = await GetQueryResult(phrase);

        var phraseDefinition = new Definition();

        string definitionsXPath = "//div[contains(@class, 'def-block ddef_block')]";
        HtmlNodeCollection? definitions = doc.DocumentNode.SelectNodes(definitionsXPath);
        string actualPhrase = doc.DocumentNode.SelectSingleNode("//div[@class='di-title']/h2/b")?.GetNodeText() ?? "";
        if (string.IsNullOrEmpty(actualPhrase)) return null;
        FillDefinitions(phraseDefinition.definitions, definitions, actualPhrase);

        // As the cambridge do not have audio for phase, we get the audio from youdao
        string audioUrl = $"https://dict.youdao.com/dictvoice?audio={Uri.EscapeDataString(actualPhrase)}&type=1";
        phraseDefinition.pronunciations.Add(new Pronunciation("", audioUrl));


        return phraseDefinition;
    }

    private void FillDefinitions(List<Meaning> definitions, HtmlNodeCollection? definitionsNodes, string word)
    {
        definitionsNodes?.ToList().ForEach(entry =>
        {
            var definition = new Meaning();
            definition.word = entry.GetSubSpanNodeText("hw dhw");
            if (string.IsNullOrEmpty(definition.word)) definition.word = word;
            definition.partOfSpeech = entry.GetSubSpanNodeText("pos dpos");
            definition.explanation = new Translation(entry.GetSubDivNodeText("def ddef_d db"),
                                                     entry.GetSubSpanNodeText("trans dtrans dtrans-se  break-cj"));


            HtmlNodeCollection? exampleNodes = entry.SelectNodes(".//div[@class='examp dexamp']");
            exampleNodes?.ToList().ForEach(node =>
            {
                var example = new Translation(node.GetSubSpanNodeText("eg deg"),
                                              node.GetSubSpanNodeText("trans dtrans dtrans-se hdb break-cj"));
                definition.examples.Add(example);
            });

            definitions.Add(definition);
        });
    }
}