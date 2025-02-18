namespace Api.Models.Database;

public partial class Transaction
{
    public string Guid { get; set; } = null!;

    public string? DateTime { get; set; }

    public string? TableName { get; set; }

    public int? RowsCount { get; set; }

    public bool? Imported { get; set; }

    public string? Reason { get; set; }

    public virtual ICollection<ImportOrder> ImportOrders { get; set; } = new List<ImportOrder>();
}
