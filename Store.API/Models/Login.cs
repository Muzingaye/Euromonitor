using System.ComponentModel.DataAnnotations;

namespace Store.API.Models
{
    public class Login : ErrorBase
    {
        [Required] public string EmailAddress { get; set; }
        [Required] public string Password { get; set; }
    }
}