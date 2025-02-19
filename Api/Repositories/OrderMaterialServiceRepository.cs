using Api.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class OrderMaterialServiceRepository(CarServiceDbContext context) : Repository<OrderMaterialService>(context)
{
    private readonly CarServiceDbContext _context = context;

    public override async Task<OrderMaterialService?> GetById(object id)
    {
        return await _context.OrderMaterialServices.Include(c => c.Order).Include(c => c.Material)
            .FirstOrDefaultAsync(c => c.Id == (int)id);
    }

    public override async Task<List<OrderMaterialService>> GetAll()
    {
        return await _context.OrderMaterialServices.Include(c => c.Order).Include(c => c.Material)
            .ToListAsync();
    }

    public async Task<List<OrderMaterialService>> GetAllByOrderId(object id)
    {
        return await _context.OrderMaterialServices.Include(c => c.Order).Include(c => c.Material)
            .Where(c => c.Id == (int)id).ToListAsync();
    }
}