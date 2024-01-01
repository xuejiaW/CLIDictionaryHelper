using System.Text;
using CLIDictionaryHelper.AnkiData;
using CLIDictionaryHelper.LexicalData;
using Newtonsoft.Json;

namespace CLIDictionaryHelper;

public static class AnkiService
{
    public static async Task AddComplexCardToAnkiAsync(WordDefinition word, string deckName, string modelName)
    {
        using var httpClient = new HttpClient();

        var note = new AnkiNote("单词 CLI", "单词", word);
        var request = new
        {
            action = "addNote",
            version = 6,
            @params = new {note}
        };

        string json = JsonConvert.SerializeObject(request);
        Console.WriteLine(json);

        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PostAsync("http://localhost:8765", content);

        if (!response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Failed to add card to Anki: {result}");
        }
        else
        {
            Console.WriteLine("Card added to Anki successfully.");

            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Successfully info: {result}");
        }
    }
}