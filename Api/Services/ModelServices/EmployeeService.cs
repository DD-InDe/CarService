﻿using Api.Models.Database;
using Api.Models.Dtos;
using Api.Models.ViewModels;
using Api.Repositories;

namespace Api.Services.ModelServices;

public class EmployeeService(EmployeeRepository employeeRepository, PersonRepository personRepository)
    : IWriteService<EmployeeDto, Employee, EmployeeViewModel>
{
    public async Task<EmployeeDto?> GetObjectById(int id)
    {
        Employee? employee = await employeeRepository.GetById(id);
        if (employee == null) return null;

        return ToDto(employee);
    }

    public async Task<EmployeeDto?> GetObjectByData(String login, String password)
    {
        Employee? employee = await employeeRepository.GetByDataEmployee(login, password);
        if (employee == null) return null;
        return ToDto(employee);
    }

    public async Task<List<EmployeeDto>> GetAllObjects()
    {
        List<Employee> employees = await employeeRepository.GetAll() ?? new();
        List<EmployeeDto> employeeDtos = new();
        employees.ForEach(c => employeeDtos.Add(ToDto(c)));
        return employeeDtos;
    }

    public async Task<bool> AddObject(EmployeeViewModel viewModel)
    {
        Employee employee = ToModel(viewModel);

        bool complete = await personRepository.Add(employee.IdNavigation);
        if (!complete) return false;

        return await employeeRepository.Add(employee);
    }

    public async Task<bool> UpdateObject(EmployeeViewModel viewModel)
    {
        Employee employee = ToModel(viewModel);

        bool complete = await personRepository.Update(employee.IdNavigation, employee.Id);
        if (!complete) return false;

        return await employeeRepository.Update(employee, employee.Id);
    }

    public async Task<bool> DeleteObject(int id)
    {
        return await personRepository.Delete(id);
    }

    public EmployeeDto ToDto(Employee model)
    {
        EmployeeDto dto = new()
        {
            Id = model.Id,
            FullName = $"{model.IdNavigation.LastName} {model.IdNavigation.FirstName} {model.IdNavigation.MiddleName}",
            Login = model.Login!,
            Password = model.Password!
        };
        return dto;
    }

    public Employee ToModel(EmployeeViewModel viewModel)
    {
        String[] fullName = viewModel.FullName.Split(' ');
        int id = viewModel.GetType() == typeof(EmployeeDto) ? ((EmployeeDto)viewModel).Id : 0;
        Employee employee = new Employee()
        {
            Id = id,
            IdNavigation = new Person()
            {
                Id = id,
                LastName = fullName[0],
                FirstName = fullName.Length > 0 ? fullName[1] : "-",
                MiddleName = fullName.Length > 1 ? fullName[2] : "-"
            },
            Login = viewModel.Login,
            Password = viewModel.Password
        };
        return employee;
    }
}