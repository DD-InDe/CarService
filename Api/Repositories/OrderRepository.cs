using Api.Models.Database;

namespace Api.Repositories;

public class OrderRepository(CarServiceDbContext context) : Repository<Order>(context)
{
    private readonly CarServiceDbContext _context = context;
}