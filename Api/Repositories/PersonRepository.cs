using Api.Models.Database;

namespace Api.Repositories;

public class PersonRepository(CarServiceDbContext context) : Repository<Person>(context)
{
    private readonly CarServiceDbContext _context = context;
}