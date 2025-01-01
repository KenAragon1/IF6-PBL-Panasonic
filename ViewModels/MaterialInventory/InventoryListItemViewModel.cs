namespace panasonic.ViewModels.MaterialInventoryViewModels;

public class InventoryListItemViewModel
{
    public int InventoryId { get; set; }
    public int MaterialNumber { get; set; }
    public string MaterialName { get; set; } = string.Empty;
    public string MaterialBarcode { get; set; } = string.Empty;
    public string MaterialDetailMeasurement { get; set; } = string.Empty;
    public int InventoryAvailableQuantity { get; set; }
    public int? ProductionLineRemark { get; set; }
}