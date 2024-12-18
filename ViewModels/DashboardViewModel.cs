using panasonic.Models;

namespace panasonic.ViewModels.DashboardViewModel;

public class IndexViewModel
{
    public List<MaterialUsedForProduction> Materials { get; set; } = new List<MaterialUsedForProduction>();
    public int MaterialCount { get; set; }
    public int ProductionLineCount { get; set; }
    public int UserCount { get; set; }
    public List<TransactionCount> TransactionCounts { get; set; } = new List<TransactionCount>();

}

public class MaterialUsedForProduction
{
    public required string MaterialName { get; set; }
    public required int QuantityUsed { get; set; }
}

public class TransactionCount
{
    public TransactionTypes Type { get; set; }
    public int Count { get; set; }
}

public class ReportViewModel
{
    public required List<MaterialTransaction> MaterialTransactions { get; set; }
}