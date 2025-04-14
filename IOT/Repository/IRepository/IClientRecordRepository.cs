using IOT.Models;
using IOT.Models.DTOs;

namespace IOT.Repository.IRepository
{
    public interface IClientRecordRepository
    {
        Task enter(string clientId);
        Task exit(string clientId);
        Task ManipulateRecord(string clientId);
        Task DeleteRecord(int recordID);
        Task<List<RecordDTO>> GetAllRecords();
        Task<List<RecordDTO>> GetClientRecord(string rfid);
        Task<ClientRecord> GetClientRecordByid(int id);


        Task Save();
    }
}
