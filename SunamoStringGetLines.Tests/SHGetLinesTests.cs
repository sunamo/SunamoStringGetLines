namespace SunamoStringGetLines.Tests;

public class SHGetLinesTests
{

    /// <summary>
    /// Vytvořeno zda správně čte různé newline
    /// Zjištěno že ano, jak File tak TF
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task ReadAllLinesTest_AllN()
    {
        var path = @"D:\_Test\PlatformIndependentNuGetPackages\SunamoFileIO\AllNN.cs";
        var o = await File.ReadAllTextAsync(path);
        var l = SHGetLines.GetLines(o);
        //var l = await TF.ReadAllLines(path);
    }

    [Fact]
    public async Task ReadAllLinesTest_AllRn()
    {
        var bp = @"D:\_Test\PlatformIndependentNuGetPackages\SunamoFileIO\";
        var path = bp + "AllRnRn.cs";
        // TF.ReadAllLines vrací 26 řádků, ReadAllLinesAsync 29
        var o = await File.ReadAllTextAsync(path);
        var l = SHGetLines.GetLines(o);
        //var l = await TF.ReadAllLines(path);
    }

    [Fact]
    public async Task ReadAllLinesTest_ProblematicFiles()
    {
        var path = @"E:\vs\Projects\PlatformIndependentNuGetPackages\SunamoLang\SunamoI18N\AppLangHelper.cs";
        // TF.ReadAllLines vrací 26 řádků, ReadAllLinesAsync 29
        var o = await File.ReadAllTextAsync(path);
        var l = SHGetLines.GetLines(o);
        //var l = await TF.ReadAllLines(path);
    }

    [Fact]
    public async Task GetLinesTest_VariousNewLinesDelimiter()
    {
        var input = "a\nc{0}\rd\r\ne";
        //var input1 = string.Format(input, "\r\n");
        var input2 = string.Format(input, "");

        //var r = SHGetLines.GetLines(input1);
        var r2 = SHGetLines.GetLines(input2);

    }
}