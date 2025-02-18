using Api.Models.Database;

namespace Api.Repositories;

public class PersonRepository(CarServiceDbContext context) : Repository<Order>(context)
{
    private readonly CarServiceDbContext _context = context;
}