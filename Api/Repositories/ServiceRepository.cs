using Api.Models.Database;

namespace Api.Repositories;

public class ServiceRepository(CarServiceDbContext context) : Repository<Service>(context)
{
    private readonly CarServiceDbContext _context = context;
}