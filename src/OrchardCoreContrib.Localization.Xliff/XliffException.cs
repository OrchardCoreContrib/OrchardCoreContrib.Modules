using System;

namespace OrchardCoreContrib.Localization.Xliff;

/// <summary>
/// Represents errors that occur during dealing with an XLIFF file.
/// </summary>
public class XliffException : Exception
{
    /// <summary>
    /// Creates a new intstance of <see cref="XliffException"/>.
    /// </summary>
    public XliffException()
    {
    }

    /// <summary>
    /// Creates a new intstance of <see cref="XliffException"/> with a specified message.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public XliffException(string message) : base(message)
    {
    }

    /// <summary>
    /// Creates a new intstance of <see cref="XliffException"/> with a specified message a reference to the inner exception that is
    /// the cause of the exception.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public XliffException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
