using AutoMapper;
using IOT.DataStore;
using IOT.Models;
using IOT.Models.DTOs;
using IOT.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace IOT.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ClientRepository(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<Client?> CreateClient(CreateClientDTO clientDto)
        {
            if (!await IsClient(clientDto.Id))
            {
                var client = mapper.Map<Client>(clientDto);
                await context.Clients.AddAsync(client);
                await Save();
                return client;
            }
            return null;
        }

        public async Task DeleteClient(int clientId)
        {
            var client = await GetClient(clientId);
            if (client != null)
            {
                context.Clients.Remove(client);
                await Save();
            }
        }

        public async Task<Client?> GetClient(int clientId)
        {
            var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == clientId);
            return client;
        }

        public async Task<List<Client>> GetClients()
        {
            var clients = await context.Clients.ToListAsync();
            return clients;
        }

        public async Task<bool> IsClient(int clientId)
        {
            var client = await GetClient(clientId);
            return client != null ? true : false;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateClient(int clientId, UpdateClientDTO clientDto)
        {
            var client = await GetClient(clientId);
            if (client != null)
            {
                mapper.Map(clientDto, client);
                context.Update(client!);
                await Save();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
