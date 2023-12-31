using HtmlAgilityPack;

namespace CLIDictionaryHelper;

public static class HtmlNodeExtensions
{
    public static string GetNodeText(this HtmlNode node) { return node.InnerText?.Trim() ?? ""; }

    public static string GetSubDivNodeText(this HtmlNode node, string className)
    {
        return node.SelectSingleNode($".//div[contains(@class, '{className}')]").GetNodeText();
    }

    public static string GetSubSpanNodeText(this HtmlNode node, string className)
    {
        return node.SelectSingleNode($".//span[contains(@class, '{className}')]").GetNodeText();
    }
}