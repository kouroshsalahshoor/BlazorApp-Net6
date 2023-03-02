
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.API.Data
{
    public class BookCreateEditDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Range(1000, int.MaxValue)]
        public int Year { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Summary { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public string? ImageName { get; set; }

        public int AuthorId { get; set; }
    }
}
