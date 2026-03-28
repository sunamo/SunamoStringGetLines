// Variable names have been checked and replaced with self-descriptive names

namespace SunamoStringGetLines.Tests;

/// <summary>
/// Tests for <see cref="SHGetLines"/> class.
/// </summary>
public class SHGetLinesTests
{
    /// <summary>
    /// Tests that GetLines correctly splits text containing only Unix newlines.
    /// </summary>
    [Fact]
    public void ReadAllLinesTest_AllN()
    {
        var text = "line1\nline2\nline3\nline4";
        var list = SHGetLines.GetLines(text);
        Assert.Equal(4, list.Count);
        Assert.Equal("line1", list[0]);
        Assert.Equal("line2", list[1]);
        Assert.Equal("line3", list[2]);
        Assert.Equal("line4", list[3]);
    }

    /// <summary>
    /// Tests that GetLines correctly splits text containing only Windows newlines.
    /// </summary>
    [Fact]
    public void ReadAllLinesTest_AllRn()
    {
        var text = "line1\r\nline2\r\nline3\r\nline4";
        var list = SHGetLines.GetLines(text);
        Assert.Equal(4, list.Count);
        Assert.Equal("line1", list[0]);
        Assert.Equal("line2", list[1]);
        Assert.Equal("line3", list[2]);
        Assert.Equal("line4", list[3]);
    }

    /// <summary>
    /// Tests that GetLines correctly handles text with mixed newline styles.
    /// </summary>
    [Fact]
    public void ReadAllLinesTest_MixedNewlines()
    {
        var text = "line1\r\nline2\nline3\rline4";
        var list = SHGetLines.GetLines(text);
        Assert.Equal(4, list.Count);
        Assert.Equal("line1", list[0]);
        Assert.Equal("line2", list[1]);
        Assert.Equal("line3", list[2]);
        Assert.Equal("line4", list[3]);
    }

    /// <summary>
    /// Tests that GetLines correctly handles various newline delimiters in a single string.
    /// </summary>
    [Fact]
    public void GetLinesTest_VariousNewLinesDelimiter()
    {
        var text = "a\nc\rd\r\ne";
        var result = SHGetLines.GetLines(text);
        Assert.Equal(4, result.Count);
        Assert.Equal("a", result[0]);
        Assert.Equal("c", result[1]);
        Assert.Equal("d", result[2]);
        Assert.Equal("e", result[3]);
    }
}
