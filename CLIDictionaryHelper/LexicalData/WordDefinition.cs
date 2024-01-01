namespace CLIDictionaryHelper.LexicalData;

public struct WordDefinition
{
    public List<Definition> definitions;

    public WordDefinition() { definitions = new List<Definition>(); }

    public bool IsComplete()
    {
        return definitions.Count != 0 && definitions.TrueForAll(definition => definition.IsComplete());
    }
}