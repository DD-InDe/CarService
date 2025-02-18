using Api.Models.Database;

namespace Api.Repositories;

public class OrderMaterialRepositoryClient(CarServiceDbContext context) : Repository<Order>(context)
{
    private readonly CarServiceDbContext _context = context;
}