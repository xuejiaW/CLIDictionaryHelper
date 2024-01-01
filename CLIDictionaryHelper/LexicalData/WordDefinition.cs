namespace CLIDictionaryHelper.LexicalData;

public struct WordDefinition
{
    public string word;
    public List<Definition> definitions;

    public WordDefinition(string word)
    {
        this.word = word;
        definitions = new List<Definition>();
    }
}