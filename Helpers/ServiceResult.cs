namespace panasonic.Helpers;

public class ServiceResult
{
    public bool Success { get; set; } = true;

    public List<string> Errors { get; set; } = new List<string>();

    public void AddError(string error)
    {
        Success = false;
        Errors.Add(error);
    }
}