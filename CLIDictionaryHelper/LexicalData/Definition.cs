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
        word= "";
        partOfSpeech = "";
        examples = new List<Translation>();
        pronunciations = new List<Pronunciation>();
    }
}