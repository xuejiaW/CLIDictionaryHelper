using CLIDictionaryHelper.LexicalData;
using HtmlAgilityPack;

namespace CLIDictionaryHelper;

public interface IDictionaryService
{
    Task<HtmlDocument> GetQueryResult(string query);
    Task<WordDefinition> GetWordDefinition(string word);
    Task<PhraseDefinition> GetPhraseDefinition(string phrase);
}