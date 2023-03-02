﻿using System.ComponentModel.DataAnnotations;

namespace BlazorApp.API.Data
{
    public class Author
    {
        public Author()
        {
            Books = new();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(250)]
        public string Bio { get; set; }

        public virtual List<Book> Books { get; set; }
    }
}
