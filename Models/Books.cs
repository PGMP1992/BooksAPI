using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EndProj_Books.Models
{
    public class Books
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string Genre { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Pages { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PublishDate { get; set; }

        [Required]
        public string ISBN { get; set; }

        public bool Complete { get; set; }

        [Required]
        public int UserId { get; set; }

        // Navigation property
        [ValidateNever]
        public User User { get; set; }
    }



}
