namespace CLIDictionaryHelper.LexicalData;

public struct WordDefinition
{
    public string query;
    public List<Definition> definitions;

    public WordDefinition(string query)
    {
        this.query = query;
        definitions = new List<Definition>();
    }
}