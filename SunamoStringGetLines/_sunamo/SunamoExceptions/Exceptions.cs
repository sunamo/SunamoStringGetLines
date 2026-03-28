namespace SunamoStringGetLines._sunamo.SunamoExceptions;

// © www.sunamo.cz. All Rights Reserved.

/// <summary>
/// Provides utility methods for exception handling and stack trace analysis.
/// </summary>
internal sealed partial class Exceptions
{
    /// <summary>
    /// Prepends a prefix to a message if the prefix is not empty.
    /// </summary>
    /// <param name="prefix">The prefix to prepend.</param>
    /// <returns>The formatted prefix string, or empty if the prefix is null or whitespace.</returns>
    internal static string CheckBefore(string prefix)
    {
        return string.IsNullOrWhiteSpace(prefix) ? string.Empty : prefix + ": ";
    }

    /// <summary>
    /// Retrieves the place of exception from the current stack trace.
    /// </summary>
    /// <param name="isFillingTypeAndMethod">Whether to extract type and method name from the first non-ThrowEx frame.</param>
    /// <returns>A tuple containing the type name, method name, and full stack trace text.</returns>
    internal static Tuple<string, string, string> PlaceOfException(bool isFillingTypeAndMethod = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var lines = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        string type = string.Empty;
        string methodName = string.Empty;
        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            if (isFillingTypeAndMethod)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out type, out methodName);
                    isFillingTypeAndMethod = false;
                }
            if (line.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, lines));
    }

    /// <summary>
    /// Extracts the type and method name from a stack trace line.
    /// </summary>
    /// <param name="stackTraceLine">A single stack trace line to parse.</param>
    /// <param name="type">The extracted type name.</param>
    /// <param name="methodName">The extracted method name.</param>
    internal static void TypeAndMethodName(string stackTraceLine, out string type, out string methodName)
    {
        var qualifiedSignature = stackTraceLine.Split("at ")[1].Trim();
        var methodPath = qualifiedSignature.Split('(')[0];
        var pathSegments = methodPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = pathSegments[^1];
        pathSegments.RemoveAt(pathSegments.Count - 1);
        type = string.Join(".", pathSegments);
    }

    /// <summary>
    /// Returns the name of the calling method at the specified stack frame depth.
    /// </summary>
    /// <param name="depth">The stack frame depth to look at.</param>
    /// <returns>The name of the calling method.</returns>
    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }

    /// <summary>
    /// Creates a custom exception message with an optional prefix.
    /// </summary>
    /// <param name="prefix">The prefix for the message.</param>
    /// <param name="message">The exception message.</param>
    /// <returns>The formatted exception message.</returns>
    internal static string? Custom(string prefix, string message)
    {
        return CheckBefore(prefix) + message;
    }
}
