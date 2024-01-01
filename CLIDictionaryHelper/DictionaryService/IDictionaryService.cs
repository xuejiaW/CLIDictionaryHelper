using CLIDictionaryHelper.LexicalData;
using HtmlAgilityPack;

namespace CLIDictionaryHelper;

public interface IDictionaryService
{
    Task<HtmlDocument> GetQueryResult(string query);
    Task<Definition?> GetWordDefinition(string word);
    Task<Definition?> GetPhraseDefinition(string phrase);
}