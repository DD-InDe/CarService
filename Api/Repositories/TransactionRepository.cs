using Api.Models.Database;

namespace Api.Repositories;

public class TransactionRepository(CarServiceDbContext context) : Repository<Transaction>(context)
{
    private readonly CarServiceDbContext _context = context;
}