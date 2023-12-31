namespace CLIDictionaryHelper;

public struct Pronunciation
{
    public string phonetic;
    public string audioUrl;

    public Pronunciation(string phonetic, string audioUrl)
    {
        this.phonetic = phonetic;
        this.audioUrl = audioUrl;
    }
}