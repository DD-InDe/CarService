using Api.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class EmployeeRepository(CarServiceDbContext context) : IRepository<Employee>
{
    public async Task<Employee?> GetById(int id)
    {
        return await context.Employees.FindAsync(id);
    }

    public async Task<Employee?> GetByDataEmployee(String login, String password)
    {
        return await context.Employees.FirstOrDefaultAsync(c => c.Login == login && c.Password == password);
    }

    public async Task<List<Employee>?> GetAll()
    {
        return await context.Employees.ToListAsync();
    }

    public async Task<bool> Add(Employee entity)
    {
        await context.Employees.AddAsync(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        Employee? employee = await context.Employees.FindAsync(id);
        if (employee == null) return false;

        context.Employees.Remove(employee);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Edit(Employee entity)
    {
        Employee? employee = await context.Employees.FindAsync(entity.Id);
        if (employee == null) return false;

        context.Employees.Update(employee);
        return await context.SaveChangesAsync() > 0;
    }
}