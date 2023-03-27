namespace SmarterSheet.Definitions.Utils;

internal static class UrlHelper
{
    public static string CreateCommaSeperatedList(this IEnumerable<ulong> numbers)
    {
        var strBuilder = new StringBuilder();

        foreach (var number in numbers)
        {
            strBuilder.Append($"{number},");
        }

        return strBuilder.ToString()[..^1];
    }

    public static string CreateUri(string path, IEnumerable<SheetInclusion>? inclusions = null)
    {
        if (inclusions == null)
            return path;

        var strBuilder = new StringBuilder("?include=");

        foreach (var inclusion in inclusions)
        {
            strBuilder.Append($"{inclusion.ToStringFast()},");
        }

        return strBuilder.ToString()[..^1];
    }

    public static string CreateUri(string path, IEnumerable<RowInclusion>? inclusions = null)
    {
        if (inclusions == null)
            return path;

        var strBuilder = new StringBuilder("?include=");

        foreach (var inclusion in inclusions)
        {
            strBuilder.Append($"{inclusion.ToStringFast()},");
        }

        return strBuilder.ToString()[..^1];
    }

    public static string CreateUrlParameter(IEnumerable<RowInclusion> inclusions)
    {
        var strBuilder = new StringBuilder("?include=");

        foreach (var inclusion in inclusions)
        {
            strBuilder.Append($"{inclusion.ToStringFast()},");
        }

        return strBuilder.ToString()[..^1];
    }

    public static string CreateUrlParameter(IEnumerable<SheetInclusion> inclusions)
    {
        var strBuilder = new StringBuilder("?include=");

        foreach (var inclusion in inclusions)
        {
            strBuilder.Append($"{inclusion.ToStringFast()},");
        }

        return strBuilder.ToString()[..^1];
    }

    public static string CreateUrlParameter(IEnumerable<FolderInclusion> inclusions)
    {
        var strBuilder = new StringBuilder("?include=");

        foreach (var inclusion in inclusions)
        {
            strBuilder.Append($"{inclusion.ToStringFast()},");
        }

        return strBuilder.ToString()[..^1];
    }

    public static string CreateUrlParameter(IEnumerable<TemplateInclusion> inclusions)
    {
        var strBuilder = new StringBuilder("?include=");

        foreach (var inclusion in inclusions)
        {
            strBuilder.Append($"{inclusion.ToStringFast()},");
        }

        return strBuilder.ToString()[..^1];
    }

    public static string CreateUrlParameter(IEnumerable<WorkspaceInclusion> inclusions)
    {
        var strBuilder = new StringBuilder("?include=");

        foreach (var inclusion in inclusions)
        {
            strBuilder.Append($"{inclusion.ToStringFast()},");
        }

        return strBuilder.ToString()[..^1];
    }

    public static string CreateUrlParameter(IEnumerable<DiscussionInclusion> inclusions)
    {
        var strBuilder = new StringBuilder("?include=");

        foreach (var inclusion in inclusions)
        {
            strBuilder.Append($"{inclusion.ToStringFast()},");
        }

        return strBuilder.ToString()[..^1];
    }
}