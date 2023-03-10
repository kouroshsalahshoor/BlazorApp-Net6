using System.ComponentModel.DataAnnotations;

namespace BlazorApp.API.Data
{
    public class AuthorCreateEditDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(150)]
        public string Bio { get; set; }
    }
}
