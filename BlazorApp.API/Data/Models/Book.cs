using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.API.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int Year { get; set; }
        public string? ISBN { get; set; }
        public string? Summary { get; set; }
        public string? Image { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int AuthorId { get; set; }
        public virtual Author? Author { get; set; }
    }
}
