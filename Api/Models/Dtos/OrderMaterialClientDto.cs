namespace Api.Models.Dtos;

public class OrderMaterialClientDto
{
    public int Id { get; set; }
    public int Count { get; set; }
    public String Name { get; set; } = default!;
}