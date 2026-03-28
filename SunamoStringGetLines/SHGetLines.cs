namespace SunamoStringGetLines;

/// <summary>
/// Provides helper methods for splitting strings into lines with various options.
/// </summary>
public class SHGetLines
{
    /// <summary>
    /// Splits a string into individual lines, handling different newline formats.
    /// </summary>
    /// <param name="text">The text to split into lines.</param>
    /// <param name="args">Optional arguments controlling line processing behavior.</param>
    /// <returns>A list of individual lines.</returns>
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
    /// If the list contains exactly one element, splits that element into lines. Otherwise returns the list as-is.
    /// </summary>
    /// <param name="list">The list of strings to process.</param>
    /// <returns>A list of individual lines.</returns>
    public static List<string> GetLinesFromLinesWithOneRow(List<string> list)
    {
        if (list.Count == 1) return GetLines(list[0]);
        return list;
    }

    /// <summary>
    /// Splits list elements that contain Unix-style newline characters into separate elements.
    /// </summary>
    /// <param name="list">The list of strings to process.</param>
    private static void SplitByUnixNewline(List<string> list)
    {
        SplitBy(list, "\r");
        SplitBy(list, "\n");
    }

    /// <summary>
    /// Splits list elements that contain the specified delimiter into separate elements.
    /// </summary>
    /// <param name="list">The list of strings to split.</param>
    /// <param name="delimiter">The delimiter string to split by.</param>
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

    /// <summary>
    /// Removes the element at the specified index and inserts the replacement list at the same position.
    /// </summary>
    /// <param name="list">The list to modify.</param>
    /// <param name="insertList">The list of elements to insert.</param>
    /// <param name="index">The index at which to perform the replacement.</param>
    private static void InsertOnIndex(List<string> list, List<string> insertList, int index)
    {
        insertList.Reverse();

        list.RemoveAt(index);

        foreach (var item in insertList) list.Insert(index, item);
    }
}
