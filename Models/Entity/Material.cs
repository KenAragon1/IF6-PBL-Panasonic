namespace panasonic.Models;

public class Material
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string QrCodeUrl { get; set; } = string.Empty;
}