namespace CLIDictionaryHelper;

public struct WordDefinition
{
    public string word;
    public List<Pronunciation> pronunciations;
    public List<Definition> definitions;

    public WordDefinition(string word)
    {
        this.word = word;
        pronunciations = new List<Pronunciation>();
        definitions = new List<Definition>();
    }
}