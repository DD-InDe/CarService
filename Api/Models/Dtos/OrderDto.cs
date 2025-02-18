namespace Api.Models.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public String DateCreate { get; set; } = "-";
    public String DateComplete { get; set; } = "-";
    public String CarBrand { get; set; } = "-";
    public String CarModel { get; set; } = "-";
    public String CarNumber { get; set; } = "-";
    public String CarVin { get; set; } = "-";
    public String Status { get; set; } = "-";
    public virtual ClientDto? Client { get; set; }
    public virtual EmployeeDto? Employee { get; set; }
    public virtual List<OrderMaterialClientDto> MaterialClient { get; set; } = new();
    public virtual List<OrderMaterialServiceDto> MaterialService { get; set; } = new();
    public virtual List<OrderServiceDto> Services { get; set; } = new();
}