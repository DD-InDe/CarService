using Api.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class EmployeeRepository(CarServiceDbContext context) : Repository<Employee>(context)
{
    private readonly CarServiceDbContext _context = context;

    public async Task<Employee?> GetByDataEmployee(String login, String password)
    {
        return await _context.Employees.Include(c => c.IdNavigation)
            .FirstOrDefaultAsync(c => c.Login == login && c.Password == password);
    }
}