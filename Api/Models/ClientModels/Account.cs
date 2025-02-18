using Api.Models.Dtos;

namespace Api.Models.ClientModels;

public class Account
{
    public EmployeeDto Employee { get; set; }
    public String Token { get; set; } = default!;
}