namespace IOT.Models.DTOs
{
    public class CreateClientDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string role { get; set; }
        //public decimal TotalPrice { get; set; } = 0;
        public string Email { get; set; }
        //public string status { get; set; } = "out";
    }
}
