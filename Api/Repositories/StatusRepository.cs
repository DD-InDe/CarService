using Api.Models.Database;

namespace Api.Repositories;

public class StatusRepository(CarServiceDbContext context) : Repository<Order>(context)
{
    private readonly CarServiceDbContext _context = context;
}