namespace SunamoStringGetLines;

using SunamoStringGetLines.SunamoArgs;

/// <summary>
/// Provides helper methods for splitting strings into lines with various options.
/// </summary>
public class SHGetLines
{
    /// <summary>
    /// Splits a string into individual lines, handling different newline formats.
    /// </summary>
    /// <param name="p">The text to split into lines.</param>
    /// <param name="a">Optional arguments controlling line processing behavior.</param>
    /// <returns>A list of individual lines.</returns>
    public static List<string> GetLines(string p, GetLinesArgs? a = null)
    {
        a ??= new GetLinesArgs();

        var parts = p.Split(new[] { "\r\n", "\n\r" }, StringSplitOptions.None).ToList();
        SplitByUnixNewline(parts);

        if (a.IsRemovingEmptyOrWhitespaceLines)
        {
            parts = parts.Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
        }

        return parts;
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

    private static void SplitByUnixNewline(List<string> d)
    {
        SplitBy(d, "\r");
        SplitBy(d, "\n");
    }

    private static void SplitBy(List<string> d, string v)
    {
        for (var i = d.Count - 1; i >= 0; i--)
        {
            if (v == "\r")
            {
                var rn = d[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                var nr = d[i].Split(new[] { "\n\r" }, StringSplitOptions.None);

                if (rn.Length > 1)
                    ThrowEx.Custom("cannot contain any \r\name, pass already split by this pattern");
                else if (nr.Length > 1) ThrowEx.Custom("cannot contain any \n\r, pass already split by this pattern");
            }

            var name = d[i].Split(new[] { v }, StringSplitOptions.None);

            if (name.Length > 1) InsertOnIndex(d, name.ToList(), i);
        }
    }

    private static void InsertOnIndex(List<string> d, List<string> r, int i)
    {
        r.Reverse();

        d.RemoveAt(i);

        foreach (var line in r) d.Insert(i, line);
    }
}