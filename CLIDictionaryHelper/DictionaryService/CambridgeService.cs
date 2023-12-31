using HtmlAgilityPack;

namespace CLIDictionaryHelper;

public class CambridgeService : IDictionaryService
{
    public async Task<HtmlDocument> GetQueryResult(string query)
    {
        string cambridgeUrl = "https://dictionary.cambridge.org/search/english-chinese-simplified/direct/?q=";
        string response = await new HttpClient().GetStringAsync($"{cambridgeUrl}{query}");

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