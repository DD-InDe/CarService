using System.Xml.Serialization;
using DatabaseLibrary.Services;

namespace DatabaseLibrary.Models;

[XmlType("order")]
public class Order : IFillable
{
    public int Id { get; set; }
    [XmlElement("clientFullName")] public string ClientFullName { get; set; } = default!;
    [XmlElement("dateCreate")] public DateTime DateCreate { get; set; }
    [XmlElement("dateComplete")] public DateTime DateComplete { get; set; }
    [XmlElement("carBrand")] public string CarBrand { get; set; } = default!;
    [XmlElement("carModel")] public string CarModel { get; set; } = default!;
    [XmlElement("govNumber")] public string GovNumber { get; set; } = default!;
    [XmlElement("carVin")] public string CarVin { get; set; } = default!;
    [XmlElement("employeeFullName")] public string EmployeeFullName { get; set; } = default!;
    [XmlElement("services")] public string Services { get; set; } = default!;
    [XmlElement("materials")] public string Materials { get; set; } = default!;
    [XmlElement("сlientMaterials")] public string ClientMaterials { get; set; } = default!;
    public string? Guid { get; set; }
    public Transaction? Transaction { get; set; }

    public void FillFromColumns(string[] columns)
    {
        if (columns.Length == 11)
        {
            ClientFullName = columns[0];
            DateCreate = Convert.ToDateTime(columns[1]);
            DateComplete = Convert.ToDateTime(columns[2]);
            CarBrand = columns[3];
            CarModel = columns[4];
            GovNumber = columns[5];
            CarVin = columns[6];
            EmployeeFullName = columns[7];
            Services = columns[8];
            Materials = columns[9];
            ClientMaterials = columns[10];
        }
    }

    public override string ToString()
    {
        StringWriter stringWriter = new StringWriter();
        stringWriter.WriteLine($"\tId: {Id}");
        stringWriter.WriteLine($"\tКлиент: {ClientFullName}");
        stringWriter.WriteLine($"\tСотрудник: {EmployeeFullName}");
        stringWriter.WriteLine($"\tДата создания: {DateCreate:d}");
        stringWriter.WriteLine($"\tДата выполнения: {DateComplete:d}");
        stringWriter.WriteLine($"\tАвтомобиль: {CarBrand} {CarModel} ({CarVin}) [{GovNumber}]");
        stringWriter.WriteLine($"\tУслуги: {Services}");
        stringWriter.WriteLine($"\tМатериалы: {Materials}");
        stringWriter.WriteLine($"\tМатериалы клиента: {ClientMaterials}");
        return stringWriter.ToString();
    }
}