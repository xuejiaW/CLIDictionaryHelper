using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using CLIDictionaryHelper.LexicalData;
using CLIDictionaryHelper.Utils;

namespace CLIDictionaryHelper.AnkiData;

[SuppressMessage("ReSharper", "NotAccessedField.Global")]
[SuppressMessage("ReSharper", "CollectionNeverQueried.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public struct AnkiNote
{
    public string deckName;
    public string modelName;
    public Dictionary<string, string> fields;
    [JsonProperty("audio")] public List<Audio> audios;
    public Options options;
    public List<string> tags;

    public AnkiNote(string deckName, string modelName, WordDefinition word)
    {
        this.deckName = deckName;
        this.modelName = modelName;
        fields = new Dictionary<string, string>();
        fields.Add("单词", word.definitions[0].word);
        fields.Add("音标", word.definitions[0].pronunciations[0].phonetic);
        fields.Add("释义", HtmlGenerator.From(word));
        var audio = new Audio(word.definitions[0].pronunciations[0].audioUrl,
                              $"CDH-{word.definitions[0].word + ".mp3"}", "发音");
        audios = new List<Audio> {audio};
        options = new Options {allowDuplicate = false};
        tags = new List<string> {"CLI"};
    }
}