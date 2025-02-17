namespace Api.Models.Dtos;

public class OrderMaterialServiceDto
{
    public MaterialDto Material { get; set; } = default!;
    public int Count { get; set; }
}