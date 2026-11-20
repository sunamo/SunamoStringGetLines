namespace SunamoStringGetLines;

/// <summary>
/// Represents the SH get lines type.
/// </summary>
public class SHGetLines
{
    /// <summary>
    /// Get lines.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    /// <returns>The get lines result.</returns>
    public static List<string> GetLines(string text, GetLinesArgs? args = null)
    {
        args ??= new GetLinesArgs();

        var lines = text.Split(new[] { "\r\n", "\n\r" }, StringSplitOptions.None).ToList();
        SplitByUnixNewline(lines);

        if (args.IsRemovingEmptyOrWhitespaceLines)
        {
            lines = lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
        }

        return lines;
    }

    /// <summary>
    /// Get lines from lines with one row.
    /// </summary>
    /// <param name="list">The list.</param>
    /// <returns>The get lines from lines with one row result.</returns>
    public static List<string> GetLinesFromLinesWithOneRow(List<string> list)
    {
        if (list.Count == 1) return GetLines(list[0]);
        return list;
    }

    private static void SplitByUnixNewline(List<string> list)
    {
        SplitBy(list, "\r");
        SplitBy(list, "\n");
    }

    private static void SplitBy(List<string> list, string delimiter)
    {
        for (var i = list.Count - 1; i >= 0; i--)
        {
            if (delimiter == "\r")
            {
                var carriageReturnNewlineParts = list[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                var newlineCarriageReturnParts = list[i].Split(new[] { "\n\r" }, StringSplitOptions.None);

                if (carriageReturnNewlineParts.Length > 1)
                    ThrowEx.Custom("cannot contain any \\r\\n, pass already split by this pattern");
                else if (newlineCarriageReturnParts.Length > 1) ThrowEx.Custom("cannot contain any \\n\\r, pass already split by this pattern");
            }

            var segments = list[i].Split(new[] { delimiter }, StringSplitOptions.None);

            if (segments.Length > 1) InsertOnIndex(list, segments.ToList(), i);
        }
    }

    private static void InsertOnIndex(List<string> list, List<string> insertList, int index)
    {
        insertList.Reverse();

        list.RemoveAt(index);

        foreach (var item in insertList) list.Insert(index, item);
    }
}
