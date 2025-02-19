using Api.Models.Database;
using Api.Models.Dtos;
using Api.Repositories;

namespace Api.Services.ModelServices;

public class ViewService(
    ClientRepository clientRepository,
    EmployeeRepository employeeRepository,
    ImportOrderRepository importOrderRepository,
    MaterialRepository materialRepository,
    OrderMaterialClientRepository orderMaterialClientRepository,
    OrderMaterialServiceRepository orderMaterialServiceRepository,
    OrderRepository orderRepository,
    OrderServiceRepository orderServiceRepository,
    ServiceRepository serviceRepository,
    TransactionRepository transactionRepository)
{
    #region Client

    public async Task<ClientDto?> GetClientById(int id)
    {
        Client? item = await clientRepository.GetById(id);
        if (item == null) return null;

        return ClientDto(item);
    }

    public async Task<List<ClientDto>> GetClients()
    {
        List<Client> items = await clientRepository.GetAll();
        List<ClientDto> itemDto = new();
        items.ForEach(c => itemDto.Add(ClientDto(c)));

        return itemDto;
    }

    private ClientDto ClientDto(Client item)
    {
        ClientDto dtoItem = new()
        {
            Email = item.Email ?? "-",
            Id = item.Id,
            Phone = item.Phone ?? "-",
            FullName =
                $"{item.IdNavigation.LastName} {item.IdNavigation.FirstName} {item.IdNavigation.MiddleName}"
        };
        return dtoItem;
    }

    #endregion

    #region Employee

    public async Task<EmployeeDto?> GetEmployeeById(int id)
    {
        Employee? item = await employeeRepository.GetById(id);
        if (item == null) return null;

        return EmployeeDto(item);
    }

    public async Task<List<EmployeeDto>> GetEmployees()
    {
        List<Employee> items = await employeeRepository.GetAll();
        List<EmployeeDto> itemDto = new();
        items.ForEach(c => itemDto.Add(EmployeeDto(c)));

        return itemDto;
    }

    private EmployeeDto EmployeeDto(Employee item)
    {
        EmployeeDto dtoItem = new()
        {
            Id = item.Id,
            FullName =
                $"{item.IdNavigation.LastName} {item.IdNavigation.FirstName} {item.IdNavigation.MiddleName}",
            Login = item.Login!,
            Password = item.Password!
        };
        return dtoItem;
    }

    #endregion

    #region ImportOrder

    public async Task<ImportOrder?> GetImportOrderById(int id)
    {
        return await importOrderRepository.GetById(id);
    }

    public async Task<List<ImportOrder>> GetImportOrder()
    {
        return await importOrderRepository.GetAll();
    }

    public async Task<List<ImportOrder>> GetImportOrderByTransactionId(String id)
    {
        return await importOrderRepository.GetAllByTransactionId(id);
    }

    #endregion

    #region Transaction

    public async Task<Transaction?> GetTransactionById(String id)
    {
        return await transactionRepository.GetById(id);
    }

    public async Task<List<Transaction>> GetTransactions()
    {
        return await transactionRepository.GetAll();
    }

    #endregion

    #region Material

    public async Task<MaterialDto?> GetMaterialById(int id)
    {
        Material? item = await materialRepository.GetById(id);
        if (item == null) return null;

        return MaterialDto(item);
    }

    public async Task<List<MaterialDto>> GetMaterials()
    {
        List<Material> items = await materialRepository.GetAll();
        List<MaterialDto> itemDto = new();
        items.ForEach(c => itemDto.Add(MaterialDto(c)));

        return itemDto;
    }

    private MaterialDto MaterialDto(Material item)
    {
        MaterialDto dto = new()
        {
            Id = item.Id,
            Name = item.Name!,
            Price = item.Price!.Value
        };
        return dto;
    }

    #endregion

    #region OrderMaterialClient

    public async Task<OrderMaterialClientDto?> GetOrderMaterialClientById(int id)
    {
        OrderMaterialClient? item = await orderMaterialClientRepository.GetById(id);
        if (item == null) return null;

        return OrderMaterialClientDto(item);
    }

    public async Task<List<OrderMaterialClientDto>> GetOrderMaterialsClient()
    {
        List<OrderMaterialClient> items = await orderMaterialClientRepository.GetAll();
        List<OrderMaterialClientDto> itemDto = new();
        items.ForEach(c => itemDto.Add(OrderMaterialClientDto(c)));

        return itemDto;
    }

    public async Task<List<OrderMaterialClientDto>> GetOrderMaterialsClientByOrderId(int id)
    {
        List<OrderMaterialClient> items = await orderMaterialClientRepository.GetAllByOrderId(id);
        List<OrderMaterialClientDto> itemDto = new();
        items.ForEach(c => itemDto.Add(OrderMaterialClientDto(c)));

        return itemDto;
    }

    private OrderMaterialClientDto OrderMaterialClientDto(OrderMaterialClient item)
    {
        OrderMaterialClientDto dtoItem = new()
        {
            Id = item.Id,
            Count = item.Count!.Value,
            Name = item.Name!
        };
        return dtoItem;
    }

    #endregion

    #region OrderMaterialService

    public async Task<OrderMaterialServiceDto?> GetOrderMaterialServiceById(int id)
    {
        OrderMaterialService? item = await orderMaterialServiceRepository.GetById(id);
        if (item == null) return null;

        return OrderMaterialServiceDto(item);
    }

    public async Task<List<OrderMaterialServiceDto>> GetOrderMaterialsService()
    {
        List<OrderMaterialService> items = await orderMaterialServiceRepository.GetAll();
        List<OrderMaterialServiceDto> itemDto = new();
        items.ForEach(c => itemDto.Add(OrderMaterialServiceDto(c)));

        return itemDto;
    }

    public async Task<List<OrderMaterialServiceDto>> GetOrderMaterialsServiceByOrderId(int id)
    {
        List<OrderMaterialService> items = await orderMaterialServiceRepository.GetAllByOrderId(id);
        List<OrderMaterialServiceDto> itemDto = new();
        items.ForEach(c => itemDto.Add(OrderMaterialServiceDto(c)));

        return itemDto;
    }

    private OrderMaterialServiceDto OrderMaterialServiceDto(OrderMaterialService item)
    {
        OrderMaterialServiceDto dtoItem = new()
        {
            Count = item.Count!.Value,
            Material = MaterialDto(item.Material!)
        };
        return dtoItem;
    }

    #endregion

    #region Service

    public async Task<ServiceDto?> GetServiceById(int id)
    {
        Service? item = await serviceRepository.GetById(id);
        if (item == null) return null;

        return ServiceDto(item);
    }

    public async Task<List<ServiceDto>> GetServices()
    {
        List<Service> items = await serviceRepository.GetAll();
        List<ServiceDto> itemDto = new();
        items.ForEach(c => itemDto.Add(ServiceDto(c)));

        return itemDto;
    }

    private ServiceDto ServiceDto(Service item)
    {
        ServiceDto dtoItem = new()
        {
            Id = item.Id,
            Name = item.Name!
        };
        return dtoItem;
    }

    #endregion

    #region OrderService

    public async Task<OrderServiceDto?> OrderServiceGetById(int id)
    {
        OrderService? item = await orderServiceRepository.GetById(id);
        if (item == null) return null;

        return OrderServiceDto(item);
    }

    public async Task<List<OrderServiceDto>> GetOrderServices()
    {
        List<OrderService> items = await orderServiceRepository.GetAll();
        List<OrderServiceDto> itemDto = new();
        items.ForEach(c => itemDto.Add(OrderServiceDto(c)));

        return itemDto;
    }

    public async Task<List<OrderServiceDto>> GetOrderServicesByOrderId(int id)
    {
        List<OrderService> items = await orderServiceRepository.GetAllByOrderId(id);
        List<OrderServiceDto> itemDto = new();
        items.ForEach(c => itemDto.Add(OrderServiceDto(c)));

        return itemDto;
    }

    private OrderServiceDto OrderServiceDto(OrderService item)
    {
        OrderServiceDto dtoItem = new()
        {
            Count = item.Count!.Value,
            Service = ServiceDto(item.Service!)
        };
        return dtoItem;
    }

    #endregion

    #region Order

    public async Task<OrderDto?> GetOrderById(int id)
    {
        Order? item = await orderRepository.GetById(id);
        if (item == null) return null;

        return await OrderDto(item);
    }

    public async Task<List<OrderDto>> GetOrders()
    {
        List<Order> items = await orderRepository.GetAll();
        List<OrderDto> itemDto = new();
        items.ForEach(async void (c) => itemDto.Add(await OrderDto(c)));

        return itemDto;
    }

    private async Task<OrderDto> OrderDto(Order item)
    {
        OrderDto dtoItem = new()
        {
            Id = item.Id,
            CarBrand = item.CarBrand!,
            CarModel = item.CarModel!,
            DateCreate = item.DateCreate!.Value,
            DateComplete = item.DateComplete!.Value,
            CarNumber = item.CarNumber!,
            CarVin = item.CarVin!,
            Status = item.Status!.Name!,
        };

        dtoItem.Client = item.Client == null ? ClientDto(item.Client!) : null;
        dtoItem.Employee = item.Employee == null ? EmployeeDto(item.Employee!) : null;
        dtoItem.MaterialClient = await GetOrderMaterialsClientByOrderId(dtoItem.Id);
        dtoItem.MaterialService = await GetOrderMaterialsServiceByOrderId(dtoItem.Id);
        dtoItem.Services = await GetOrderServicesByOrderId(dtoItem.Id);

        return dtoItem;
    }

    #endregion
}