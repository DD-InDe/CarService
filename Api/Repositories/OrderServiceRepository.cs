using Api.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class OrderServiceRepository(CarServiceDbContext context) : Repository<OrderService>(context)
{
    private readonly CarServiceDbContext _context = context;

    public override async Task<OrderService?> GetById(object id)
    {
        return await _context.OrderServices.Include(c => c.Service).Include(c => c.Executor)
            .FirstOrDefaultAsync(c => c.Id == (int)id);
    }

    public override async Task<List<OrderService>> GetAll()
    {
        return await _context.OrderServices.Include(c => c.Service).Include(c => c.Executor)
            .ToListAsync();
    }
    
    public async Task<List<OrderService>> GetAllByOrderId(object id)
    {
        return await _context.OrderServices.Include(c => c.Service).Include(c => c.Executor)
            .Where(c=>c.OrderId == (int)id)
            .ToListAsync();
    }
}