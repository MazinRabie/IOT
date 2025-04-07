namespace IOT.Models
{
    public class ClientRecord
    {
        public int Id { get; set; }
        public string RFID { get; set; }
        public DateTime EntranaceTime { get; set; }
        public DateTime ExitTime { get; set; }
        public decimal Price { get; set; }
        public Client Client { get; set; }
    }
}
