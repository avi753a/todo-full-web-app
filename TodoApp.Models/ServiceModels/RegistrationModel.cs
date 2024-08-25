using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class RegistrationModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

