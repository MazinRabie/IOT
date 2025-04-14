using System.ComponentModel.DataAnnotations;

namespace IOT.Models.DTOs
{
    public class CreateClientDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string role { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
