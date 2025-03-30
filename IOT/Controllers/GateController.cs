using IOT.Models.DTOs;
using IOT.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace IOT.Controllers
{
    [ApiController]
    [Route("api/gate")]
    public class GateController : ControllerBase
    {
        private readonly IClientRepository clientRepository;
        private readonly IClientRecordRepository recordRepository;

        public GateController(IClientRepository clientRepository, IClientRecordRepository recordRepository)
        {
            this.clientRepository = clientRepository;
            this.recordRepository = recordRepository;
        }

        [HttpGet("GetClients")]
        public async Task<IActionResult> GetClients()
        {
            var clients = await clientRepository.GetClients();
            return Ok(clients);
        }

        [HttpGet("GetClient/{id:int}")]
        public async Task<IActionResult> GetClient([FromRoute] int id)
        {
            var client = await clientRepository.GetClient(id);
            if (client == null) return BadRequest("not found");
            return Ok(client);
        }

        [HttpPost("CreateClient")]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDTO createClientDTO)
        {
            var client = await clientRepository.CreateClient(createClientDTO);
            if (client == null) return BadRequest("Client with same id already exists");
            return Ok(client);
        }

        [HttpPut("updateClient /{id:int}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] UpdateClientDTO updateClientDTO)
        {
            if (await clientRepository.IsClient(id))
                await clientRepository.UpdateClient(id, updateClientDTO);
            return Ok();
        }

        [HttpDelete("DeleteClient/{id:int}")]
        public async Task<IActionResult> deleteClient([FromRoute] int id)
        {
            if (await clientRepository.IsClient(id))
            {
                await clientRepository.DeleteClient(id);
                return Ok();
            }
            else return BadRequest($"no such client  with that id {id}");
        }


        [HttpGet("IsClient/{id:int}")]
        public async Task<bool> IsClient([FromRoute] int id)
        {
            return await clientRepository.IsClient(id);
        }
        [HttpPut("Scan/{rfid:int}")]
        // scan rfid 2- check isclient 3- if true open gate 4- add record in the database 5-scan for exit 6-update record
        public async Task<bool> Scan([FromRoute] int rfid)
        {
            if (!await IsClient(rfid)) return false;
            await recordRepository.ManipulateRecord(rfid);
            return true;
        }
    }
}
