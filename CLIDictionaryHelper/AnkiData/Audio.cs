namespace CLIDictionaryHelper.AnkiData;

public struct Audio
{
    public string url { get; set; }
    public string filename { get; set; }
    public string[] fields { get; set; }

    public Audio(string url, string filename, string field)
    {
        this.url = url;
        this.filename = filename;
        this.fields = new[] {field};
    }
}