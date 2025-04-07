using IOT.Models;
using IOT.Models.DTOs;

namespace IOT.Repository.IRepository
{
    public interface IClientRepository
    {
        Task<List<Client>> GetClients();
        Task<Client> GetClient(string clientId);
        Task<Client> CreateClient(CreateClientDTO clientDto);
        Task UpdateClient(string clientId, UpdateClientDTO clientDto);
        Task DeleteClient(string clientId);
        Task<bool> IsClient(string clientId);
        Task Save();



    }
}
