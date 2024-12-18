using panasonic.Models;

namespace panasonic.ViewModels.ReportViewModel;

public class IndexViewModel
{
    public required List<MaterialTransaction> MaterialTransactions { get; set; }
}

public class DetailViewModel
{
    public required MaterialTransaction MaterialTransaction { get; set; }

}