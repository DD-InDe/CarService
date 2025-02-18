using Api.Models.Database;

namespace Api.Repositories;

public class OrderServiceRepository(CarServiceDbContext context) : Repository<Order>(context)
{
    private readonly CarServiceDbContext _context = context;
}