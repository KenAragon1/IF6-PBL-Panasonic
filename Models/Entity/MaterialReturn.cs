namespace panasonic.Models;

public class MaterialReturn
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public int Quantity {get; set;}
    public string ReasonReturn {get; set;} = string.Empty;
    public string Status {get; set;} = string.Empty;
    public int Date {get; set;}
}
