using Api.Models.Database;
using Api.Models.Dtos;
using Api.Models.ViewModels;
using Api.Repositories;

namespace Api.Services.ModelServices;

public class ClientService(ClientRepository clientRepository, PersonRepository personRepository)
    : IReadService<ClientDto, Client, ClientViewModel>
{
    public async Task<ClientDto?> GetObjectById(int id)
    {
        Client? client = await clientRepository.GetById(id);
        if (client == null) return null;

        return ToDto(client);
    }

    public async Task<List<ClientDto>> GetAllObjects()
    {
        List<Client> clients = await clientRepository.GetAll();
        List<ClientDto> clientDtos = new();
        clients.ForEach(c => clientDtos.Add(ToDto(c)));

        return clientDtos;
    }

    public ClientDto ToDto(Client model)
    {
        ClientDto clientDto = new ClientDto()
        {
            Id = model.Id,
            Email = model.Email ?? "-",
            Phone = model.Phone ?? "-",
            FullName = $"{model.IdNavigation.LastName} {model.IdNavigation.FirstName} {model.IdNavigation.MiddleName}"
        };
        return clientDto;
    }

    // public async Task<bool> AddObject(ClientViewModel newObject)
    // {
    //     Client client = ToModel(newObject);
    //     bool complete = await personRepository.Add(client.IdNavigation);
    //     if (complete) return await clientRepository.Add(client);
    //     return false;
    // }
    //
    // public async Task<bool> UpdateObject(ClientViewModel viewModel)
    // {
    //     Client client = ToModel(viewModel);
    //
    //     return await clientRepository.Update(client, client.Id);
    // }
    //
    // public async Task<bool> DeleteObject(int id)
    // {
    //     return await personRepository.Delete(id);
    // }

    public Client ToModel(ClientViewModel viewModel)
    {
        String[] fullName = viewModel.FullName.Split(' ');
        int id = viewModel.GetType() == typeof(ClientDto) ? ((ClientDto)viewModel).Id : 0;
        Client client = new Client()
        {
            Id = id,
            IdNavigation = new Person()
            {
                Id = id,
                LastName = fullName[0],
                FirstName = fullName.Length > 0 ? fullName[1] : "-",
                MiddleName = fullName.Length > 1 ? fullName[2] : "-"
            },
            Email = viewModel.Email,
            Phone = viewModel.Phone
        };
        return client;
    }
}