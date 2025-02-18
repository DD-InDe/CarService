using Api.Models.Database;

namespace Api.Models.Dtos;

public class ClientDto()
{
    public int Id { get; set; }
    public String Email { get; set; } = "-";
    public String Phone { get; set; } = "-";
    public String FullName { get; set; } = "-";
}