namespace CLIDictionaryHelper;

public struct Definition
{
    public string query;
    public string partOfSpeech;
    public Translation explanation;
    public List<Pronunciation> pronunciations;
    public List<Translation> examples;

    public Definition()
    {
        query= "";
        partOfSpeech = "";
        examples = new List<Translation>();
        pronunciations = new List<Pronunciation>();
    }
}