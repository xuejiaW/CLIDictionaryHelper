namespace CLIDictionaryHelper.LexicalData;

public struct Definition
{
    public string word;
    public string partOfSpeech;
    public Translation explanation;
    public List<Pronunciation> pronunciations;
    public List<Translation> examples;

    public Definition()
    {
        word = "";
        partOfSpeech = "";
        examples = new List<Translation>();
        pronunciations = new List<Pronunciation>();
    }

    public bool IsComplete()
    {
        return word != "" && partOfSpeech != "" &&
               examples.Count != 0 && pronunciations.Count != 0 &&
               explanation.IsComplete();
    }
}