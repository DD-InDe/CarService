namespace DatabaseLibrary.Models;

public class Transaction
{
    public string Guid { get; set; }
    public DateTime DateTime { get; set; }
    public string TableName { get; set; }
    public int RowsCount { get; set; }

    public override string ToString()
    {
        StringWriter stringWriter = new StringWriter();
        stringWriter.WriteLine($"GUID: {Guid}");
        stringWriter.WriteLine($"Дата и время: {DateTime:g}");
        stringWriter.WriteLine($"Таблица: {TableName}");
        stringWriter.WriteLine($"Количество записей: {RowsCount}");
        return stringWriter.ToString();
    }
}