using Api.Models.Database;

namespace Api.Repositories;

public class ServiceRepository(CarServiceDbContext context) : Repository<Order>(context)
{
    private readonly CarServiceDbContext _context = context;
}