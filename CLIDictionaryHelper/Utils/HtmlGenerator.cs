using System.Text;
using CLIDictionaryHelper.LexicalData;

namespace CLIDictionaryHelper.Utils;

public static class HtmlGenerator
{
    private static string? s_StyleStr = null;
    private static string styleStr => s_StyleStr ??= ResourcesLoader.ReadFile("AnkiStyle.css");

    public static string From(WordDefinition word)
    {
        var sb = new StringBuilder();
        sb.AppendLine("<style>");
        sb.AppendLine(styleStr);
        sb.AppendLine("</style>");

        word.definitions.ForEach(definition =>
        {
            sb.AppendLine($"<span class=\"pos\">{definition.partOfSpeech.ToUpper()}</span>");
            sb.AppendLine("<span class=\"tran\">");
            sb.AppendLine($"    <span class=\"eng_tran\">{definition.explanation.originText}</span>");
            sb.AppendLine($"    <span class=\"chn_tran\">{definition.explanation.translatedText}</span>");
            sb.AppendLine("</span>");

            sb.AppendLine("<ul class=\"sents\">");
            definition.examples.ForEach(example =>
            {
                sb.AppendLine("    <li class=\"sent\">");
                sb.AppendLine($"        <span class=\"eng_sent\">{example.originText}</span>");
                sb.AppendLine($"        <span class=\"chn_sent\">{example.translatedText}</span>");
                sb.AppendLine("    </li>");
            });

            sb.AppendLine("</ul>");
            sb.AppendLine("<hr />");
        });

        return sb.ToString();
    }
}