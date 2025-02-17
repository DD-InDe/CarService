using System.Xml.Serialization;

namespace DatabaseLibrary.Models;

[XmlRoot("item")]
public class OrderList
{
    [XmlElement("order")] public List<Order> Orders { get; set; }
}