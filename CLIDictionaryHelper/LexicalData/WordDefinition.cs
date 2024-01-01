namespace CLIDictionaryHelper.LexicalData;

public struct WordDefinition
{
    public List<Definition> definitions;
    public List<Pronunciation> pronunciations;

    public WordDefinition()
    {
        definitions = new List<Definition>();
        pronunciations = new List<Pronunciation>();
    }

    public bool IsComplete()
    {
        return definitions.Count != 0 && definitions.TrueForAll(definition => definition.IsComplete());
    }
}