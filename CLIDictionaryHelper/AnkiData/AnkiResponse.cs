using Newtonsoft.Json;

namespace CLIDictionaryHelper.AnkiData;

public struct AnkiResponse
{
    [JsonProperty("result")]
    public string[] result { get; set; }

    [JsonProperty("error")]
    public object error { get; set; }
}