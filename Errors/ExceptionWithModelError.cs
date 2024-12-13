namespace panasonic.Errors;

public class ExceptionWithModelError : Exception
{
    public string ModelKey { get; set; }
    public ExceptionWithModelError(string modelKey, string message) : base(message)
    {
        ModelKey = modelKey;
    }
}