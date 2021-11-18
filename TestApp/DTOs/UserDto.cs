using System.ComponentModel.DataAnnotations;

namespace TestApp.DTOs
{
    public class UserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
