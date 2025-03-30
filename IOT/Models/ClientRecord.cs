namespace IOT.Models
{
    public class ClientRecord
    {
        public int Id { get; set; }
        public int RFID { get; set; }
        public DateTime EntranaceTime { get; set; }
        public DateTime ExitTime { get; set; }
        public decimal Price { get; set; }
        public Client Client { get; set; }
    }
}
