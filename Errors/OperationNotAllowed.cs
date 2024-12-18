namespace panasonic.Exceptions;

public class OperationNotAllowed : Exception
{
    public OperationNotAllowed(string message) : base(message)
    {
    }
}