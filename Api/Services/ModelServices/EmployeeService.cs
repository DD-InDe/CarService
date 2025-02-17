using Api.Models.Database;
using Api.Models.Dtos;
using Api.Repositories;

namespace Api.Services.ModelServices;

public class EmployeeService(EmployeeRepository repository) : IModelService<EmployeeDto, Employee>
{
    public async Task<EmployeeDto> GetObjectById(int id)
    {
        return ToDto(await repository.GetById(id) ?? throw new ArgumentNullException());
    }

    public async Task<EmployeeDto> GetObjectByData(String login, String password)
    {
        return ToDto(await repository.GetByDataEmployee(login, password) ?? throw new ArgumentNullException());
    }

    public async Task<List<EmployeeDto>> GetAllObjects()
    {
        List<Employee> employees = await repository.GetAll() ?? new();
        List<EmployeeDto> employeeDtos = new();
        employees.ForEach(c => employeeDtos.Add(ToDto(c)));
        return employeeDtos;
    }

    public async Task<bool> AddObject(EmployeeDto newObject)
    {
        return await repository.Add(FromDto(newObject));
    }

    public async Task<bool> UpdateObject(EmployeeDto newObject)
    {
        return await repository.Update(FromDto(newObject));
    }

    public async Task<bool> DeleteObject(int id)
    {
        return await repository.Delete(id);
    }

    public EmployeeDto ToDto(Employee model)
    {
        EmployeeDto dto = new EmployeeDto()
        {
            Id = model.Id,
            FullName = $"{model.IdNavigation.LastName} {model.IdNavigation.FirstName} {model.IdNavigation.MiddleName}",
            Login = model.Login!,
            Password = model.Password!
        };
        return dto;
    }

    public Employee FromDto(EmployeeDto dto)
    {
        String[] fullName = dto.FullName.Split(' ');
        Employee employee = new Employee()
        {
            Id = dto.Id,
            IdNavigation = new Person()
            {
                Id = dto.Id,
                LastName = fullName[0],
                FirstName = fullName.Length > 0 ? fullName[1] : "-",
                MiddleName = fullName.Length > 1 ? fullName[2] : "-"
            },
            Login = dto.Login,
            Password = dto.Password
        };
        return employee;
    }
}