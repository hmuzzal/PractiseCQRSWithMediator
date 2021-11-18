using System.ComponentModel.DataAnnotations;

namespace TestApp.Model
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNUmber { get; set; }
    }
}
