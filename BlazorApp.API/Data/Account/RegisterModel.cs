
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.API.Data
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
