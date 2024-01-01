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

    public async Task<WordDefinition> GetWordDefinition(string word)
    {
        HtmlDocument doc = await GetQueryResult(word);

        var wordDefinition = new WordDefinition(word);

        string definitionsXPath = "//div[contains(@class, 'entry-body')]/*[contains(@class, 'entry-body__el')]";
        HtmlNodeCollection? entries = doc.DocumentNode.SelectNodes(definitionsXPath);
        entries?.ToList().ForEach(entry =>
        {
            var definition = new Definition();
            definition.query = entry.GetSubSpanNodeText("hw dhw");
            definition.partOfSpeech = entry.GetSubSpanNodeText("pos dpos");
            definition.explanation = new Translation(entry.GetSubDivNodeText("def ddef_d db"),
                                                     entry.GetSubSpanNodeText("trans dtrans dtrans-se  break-cj"));

            HtmlNodeCollection phonetics = entry.SelectNodes(".//span[contains(@class, 'pron dpron')]");
            HtmlNodeCollection audioUrls = entry.SelectNodes(".//source[@type='audio/mpeg']");

            Debug.Assert(phonetics.Count == audioUrls.Count);
            for (int i = 0; i < phonetics.Count; i++)
            {
                string phonetic = phonetics[i].GetNodeText();
                string audioUrl = $"{k_CambridgeUrl}{audioUrls[i].GetAttributeValue("src", "")}";
                definition.pronunciations.Add(new Pronunciation(phonetic, audioUrl));
            }

            HtmlNodeCollection? exampleNodes = entry.SelectNodes(".//div[@class='examp dexamp']");
            exampleNodes?.ToList().ForEach(node =>
            {
                var example = new Translation(node.GetSubSpanNodeText("eg deg"),
                                              node.GetSubSpanNodeText("trans dtrans dtrans-se hdb break-cj"));
                definition.examples.Add(example);
            });

            wordDefinition.definitions.Add(definition);
        });
        return wordDefinition;
    }

    public Task<PhraseDefinition> GetPhraseDefinition(string phrase) { throw new NotImplementedException(); }
}