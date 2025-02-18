namespace Api.Models.Dtos;

public class OrderServiceDto
{
    public EmployeeDto Employee { get; set; } = default!;
    public ServiceDto Service { get; set; } = default!;
    public int Count;
}