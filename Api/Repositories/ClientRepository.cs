using Api.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class ClientRepository(CarServiceDbContext context) : Repository<Client>(context)
{
    private CarServiceDbContext _context = context;

    public override async Task<Client?> GetById(object id)
    {
        return await context.Clients.Include(c => c.IdNavigation).FirstOrDefaultAsync(c => c.Id == (int)id);
    }

    public override async Task<List<Client>> GetAll()
    {
        return await context.Clients.Include(c => c.IdNavigation).ToListAsync();
    }
}