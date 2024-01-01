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

    public AnkiNote(string deckName, string modelName, Definition definition)
    {
        this.deckName = deckName;
        this.modelName = modelName;
        fields = new Dictionary<string, string>();
        fields.Add("单词", definition.definitions[0].word);
        fields.Add("释义", HtmlGenerator.From(definition));
        audios = new List<Audio>();
        if (definition.pronunciations.Count > 0)
        {
            fields.Add("音标", definition.pronunciations[0].phonetic);
            var audio = new Audio(definition.pronunciations[0].audioUrl,
                                  $"CDH-{definition.definitions[0].word + ".mp3"}", "发音");
            audios.Add(audio);
        }

        options = new Options {allowDuplicate = false};
        tags = new List<string> {"CLI"};
    }
}