namespace CLIDictionaryHelper.LexicalData;

public struct Definition
{
    public string word;
    public string partOfSpeech;
    public Translation explanation;
    public List<Translation> examples;

    public Definition()
    {
        word = "";
        partOfSpeech = "";
        examples = new List<Translation>();
    }

    public bool IsComplete()
    {
        return word != "" && partOfSpeech != "" &&
               examples.Count != 0 &&
               explanation.IsComplete();
    }
}