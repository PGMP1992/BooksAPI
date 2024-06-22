using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndProj_Books.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public ICollection<Books> Books { get; set; }
    }
}
