namespace CLIDictionaryHelper.LexicalData;

public struct Translation
{
    public string originText;
    public string translatedText;

    public Translation(string originText, string translatedText)
    {
        this.originText = originText;
        this.translatedText = translatedText;
    }
}