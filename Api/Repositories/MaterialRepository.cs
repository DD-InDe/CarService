using Api.Models.Database;

namespace Api.Repositories;

public class MaterialRepository(CarServiceDbContext context) : Repository<Material>(context)
{
    private readonly CarServiceDbContext _context = context;
}