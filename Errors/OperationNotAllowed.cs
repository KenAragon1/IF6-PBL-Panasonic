namespace panasonic.Exceptions;

public class OperationNotAllowed : Exception
{
    public string ModelErrorKey { get; set; } = string.Empty;
    public OperationNotAllowed(string message, string? modelErrorKey = null) : base(message)
    {
        if (modelErrorKey != null) ModelErrorKey = modelErrorKey;
    }
}