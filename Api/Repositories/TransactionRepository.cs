using Api.Models.Database;

namespace Api.Repositories;

public class TransactionRepository(CarServiceDbContext context) : Repository<Order>(context)
{
    private readonly CarServiceDbContext _context = context;
}