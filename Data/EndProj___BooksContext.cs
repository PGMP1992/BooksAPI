using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EndProj_Books.Models;

namespace EndProj___Books.Data
{
    public class EndProj___BooksContext : DbContext
    {
        public EndProj___BooksContext(DbContextOptions<EndProj___BooksContext> options)
            : base(options)
        {
        }

        public DbSet<EndProj_Books.Models.Books> Book { get; set; } = default!;
    }
}
