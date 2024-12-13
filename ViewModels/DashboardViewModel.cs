using panasonic.Models;

namespace panasonic.ViewModels.DashboardViewModel;

public class ReportViewModel
{
    public required List<MaterialTransaction> MaterialTransactions { get; set; }
}