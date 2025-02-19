using Api.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class OrderRepository(CarServiceDbContext context) : Repository<Order>(context)
{
    private readonly CarServiceDbContext _context = context;

    public override async Task<Order?> GetById(object id)
    {
        return await _context.Orders.Include(c => c.Employee).Include(c => c.Client).Include(c => c.Status)
            .FirstOrDefaultAsync(c => c.Id == (int)id);
    }

    public override async Task<List<Order>> GetAll()
    {
        return await _context.Orders.Include(c => c.Employee).Include(c => c.Client).Include(c => c.Status)
            .ToListAsync();
    }
}