namespace CLIDictionaryHelper.LexicalData;

public struct Definition
{
    public List<Meaning> definitions;
    public List<Pronunciation> pronunciations;

    public Definition()
    {
        definitions = new List<Meaning>();
        pronunciations = new List<Pronunciation>();
    }

    public bool IsComplete()
    {
        return definitions.Count != 0 && definitions.TrueForAll(definition => definition.IsComplete());
    }
}