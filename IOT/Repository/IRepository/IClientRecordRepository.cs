namespace IOT.Repository.IRepository
{
    public interface IClientRecordRepository
    {
        Task enter(string clientId);
        Task exit(string clientId);
        Task ManipulateRecord(string clientId);
        Task DeleteRecord(int recordID);
        Task Save();
    }
}
