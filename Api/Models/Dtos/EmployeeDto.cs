namespace Api.Models.Dtos;

public class EmployeeDto
{
    public int Id { get; set; }
    public String FullName { get; set; } = "-";
    public String Login { get; set; } = "-";
    public String Password { get; set; } = "-";
}