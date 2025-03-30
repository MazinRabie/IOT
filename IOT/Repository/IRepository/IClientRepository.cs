using IOT.Models;
using IOT.Models.DTOs;

namespace IOT.Repository.IRepository
{
    public interface IClientRepository
    {
        Task<List<Client>> GetClients();
        Task<Client> GetClient(int clientId);
        Task<Client> CreateClient(CreateClientDTO clientDto);
        Task UpdateClient(int clientId, UpdateClientDTO clientDto);
        Task DeleteClient(int clientId);
        Task<bool> IsClient(int clientId);
        Task Save();



    }
}
