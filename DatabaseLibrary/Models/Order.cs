using System.Xml.Serialization;
using DatabaseLibrary.Services;

namespace DatabaseLibrary.Models;

[XmlType("order")]
public class Order : IFillable
{
    public int Id { get; set; }
    [XmlElement("clientFullName")] public string ClientFullName { get; set; }
    [XmlElement("dateCreate")] public DateTime DateCreate { get; set; }
    [XmlElement("dateComplete")] public DateTime DateComplete { get; set; }
    [XmlElement("carBrand")] public string CarBrand { get; set; }
    [XmlElement("carModel")] public string CarModel { get; set; }
    [XmlElement("govNumber")] public string GovNumber { get; set; }
    [XmlElement("carVin")] public string CarVin { get; set; }
    [XmlElement("employeeFullName")] public string EmployeeFullName { get; set; }
    [XmlElement("services")] public string Services { get; set; }
    [XmlElement("materials")] public string Materials { get; set; }
    [XmlElement("сlientMaterials")] public string ClientMaterials { get; set; }
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
        else throw new Exception("Ошибка при заполнении данных из полей");
    }
}