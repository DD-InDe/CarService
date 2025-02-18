using Api.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class ImportOrderRepository(CarServiceDbContext context) : Repository<ImportOrder>(context)
{
    private readonly CarServiceDbContext _context = context;
}