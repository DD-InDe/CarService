using Api.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class ImportOrderRepository(CarServiceDbContext context) : Repository<ImportOrder>(context)
{
    private readonly CarServiceDbContext _context = context;

    public override async Task<ImportOrder?> GetById(object id)
    {
        return await context.ImportOrders.Include(c => c.Gu).FirstOrDefaultAsync(c => c.Id == (int)id);
    }

    public override async Task<List<ImportOrder>> GetAll()
    {
        return await context.ImportOrders.Include(c => c.Gu).ToListAsync();
    }

    public async Task<List<ImportOrder>> GetAllByTransactionId(object id)
    {
        return await context.ImportOrders.Include(c => c.Gu).Where(c => c.Guid == (string)id).ToListAsync();
    }
}