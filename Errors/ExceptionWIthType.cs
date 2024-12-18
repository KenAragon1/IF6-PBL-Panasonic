public enum ExceptionTypes
{
    DataNotFound, ConditionNotMet, UniqueColumn

}

public class ExceptionWithType : Exception
{
    public ExceptionTypes Type { get; set; }
    public ExceptionWithType(ExceptionTypes type, string message) : base(message)
    {
        Type = type;
    }
}