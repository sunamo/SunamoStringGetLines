namespace SunamoStringGetLines.SunamoArgs;

/// <summary>
/// Provides configuration arguments for the GetLines method.
/// </summary>
public class GetLinesArgs
{
    /// <summary>
    /// Gets or sets a value indicating whether empty or whitespace-only lines should be removed from the result.
    /// </summary>
    public bool IsRemovingEmptyOrWhitespaceLines { get; set; } = false;
}
