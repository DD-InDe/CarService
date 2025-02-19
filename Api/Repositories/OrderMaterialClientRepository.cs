using Api.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class OrderMaterialClientRepository(CarServiceDbContext context) : Repository<OrderMaterialClient>(context)
{
    private readonly CarServiceDbContext _context = context;

    public override async Task<OrderMaterialClient?> GetById(object id)
    {
        return await _context.OrderMaterialClients.Include(c => c.Order).FirstOrDefaultAsync(c => c.Id == (int)id);
    }

    public override async Task<List<OrderMaterialClient>> GetAll()
    {
        return await _context.OrderMaterialClients.Include(c => c.Order).ToListAsync();
    }
    
    public async Task<List<OrderMaterialClient>> GetAllByOrderId(int id)
    {
        return await _context.OrderMaterialClients.Include(c => c.Order).Where(c=>c.OrderId==id).ToListAsync();
    }
}