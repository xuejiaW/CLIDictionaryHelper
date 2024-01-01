namespace CLIDictionaryHelper.AnkiData;

public struct AnkiRequest
{
    public string action;
    public string version;
    public AnkiRequestParams @params;

    public AnkiRequest(string action, string version, AnkiRequestParams @params)
    {
        this.action = action;
        this.version = version;
        this.@params = @params;
    }
}