using System.Text.Json.Serialization;

namespace IOT.Models
{
    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string role { get; set; }
        public decimal? TotalPrice { get; set; } = 0;
        public string status { get; set; } = "out";
        public string? Email { get; set; }
        [JsonIgnore]
        public List<ClientRecord> clientRecords { get; set; }


    }
}
