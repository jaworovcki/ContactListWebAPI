using System.ComponentModel.DataAnnotations;

namespace ContactListWebAPI.Models
{
    public class Person
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(150)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(150)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
