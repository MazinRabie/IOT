namespace IOT.Models.DTOs
{
    public class RecordDTO
    {
        public int Id { get; set; }
        public string RFID { get; set; }
        public DateTime EntranaceTime { get; set; }
        public DateTime ExitTime { get; set; }
        public decimal Price { get; set; }
    }
}
