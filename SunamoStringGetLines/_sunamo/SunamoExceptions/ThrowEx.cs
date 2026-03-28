namespace SunamoStringGetLines._sunamo.SunamoExceptions;

// Variable names have been checked and replaced with self-descriptive names

/// <summary>
/// Provides methods for throwing exceptions with detailed context information.
/// </summary>
internal partial class ThrowEx
{
    /// <summary>
    /// Throws a custom exception with the specified message.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception or just return true.</param>
    /// <param name="secondMessage">An optional additional message to append.</param>
    /// <returns>True if the exception message was generated, false otherwise.</returns>
    internal static bool Custom(string message, bool isReallyThrowing = true, string secondMessage = "")
    {
        string joined = string.Join(" ", message, secondMessage);
        string? exceptionText = Exceptions.Custom(FullNameOfExecutedCode(), joined);
        return ThrowIsNotNull(exceptionText, isReallyThrowing);
    }

    /// <summary>
    /// Returns the full name of the currently executed code location.
    /// </summary>
    /// <returns>The fully qualified name of the current execution point.</returns>
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    /// <summary>
    /// Returns the full name of the executed code based on the type and method name.
    /// </summary>
    /// <param name="type">The type or object representing the declaring type.</param>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="isFromThrowEx">Whether the call originates from ThrowEx.</param>
    /// <returns>The fully qualified method name.</returns>
    private static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (isFromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type typeObject)
        {
            typeFullName = typeObject.FullName ?? "Type cannot be get via type is Type typeObject";
        }
        else if (type is MethodBase method)
        {
            typeFullName = method.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase method";
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type objectType = type.GetType();
            typeFullName = objectType.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    /// <summary>
    /// Throws an exception if the exception message is not null.
    /// </summary>
    /// <param name="exception">The exception message to evaluate.</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception.</param>
    /// <returns>True if the exception message was not null, false otherwise.</returns>
    internal static bool ThrowIsNotNull(string? exception, bool isReallyThrowing = true)
    {
        if (exception != null)
        {
            Debugger.Break();
            if (isReallyThrowing)
            {
                throw new Exception(exception);
            }
            return true;
        }
        return false;
    }
}
