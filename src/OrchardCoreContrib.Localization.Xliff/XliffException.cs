using System;

namespace OrchardCoreContrib.Localization.Xliff;

public class XliffException : Exception
{
    public XliffException()
    {
    }

    public XliffException(string message) : base(message)
    {
    }

    public XliffException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
