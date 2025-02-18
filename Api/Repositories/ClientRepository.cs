using Api.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class ClientRepository(CarServiceDbContext context) : Repository<Client>(context)
{
    private CarServiceDbContext _context = context;
}