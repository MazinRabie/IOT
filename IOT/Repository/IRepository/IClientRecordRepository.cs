namespace IOT.Repository.IRepository
{
    public interface IClientRecordRepository
    {
        Task enter(int clientId);
        Task exit(int clientId);
        Task ManipulateRecord(int clientId);
        Task DeleteRecord(int recordID);
        Task Save();
    }
}
