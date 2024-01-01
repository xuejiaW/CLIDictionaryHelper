namespace CLIDictionaryHelper.AnkiData;

public struct AnkiRequestParams
{
    public AnkiNote note;

    public AnkiRequestParams(AnkiNote note) { this.note = note; }
}