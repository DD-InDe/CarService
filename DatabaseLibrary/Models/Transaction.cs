namespace DatabaseLibrary.Models;

public class Transaction
{
    public string Guid { get; set; }
    public DateTime DateTime { get; set; }
    public string TableName { get; set; }
    public int RowsCount { get; set; }
}