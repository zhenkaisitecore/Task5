using System.ComponentModel.DataAnnotations;

namespace Task5.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="User name are required!")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Password are required!")]
        [MinLength(5, ErrorMessage ="The password must have more than 5 char!")]
        public string Password { get; set; }
    }
}