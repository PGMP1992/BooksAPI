using Microsoft.EntityFrameworkCore;

namespace EndProj_Books.Models
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {
        }

        public DbSet<Books> Books { get; set; }
        public DbSet<User> User { get; set; }  // Use the correct table name

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Books>()
                .Property(b => b.Complete)
                .IsRequired();

            modelBuilder.Entity<Books>()
                .HasOne(b => b.User)
                .WithMany(u => u.Books)
                .HasForeignKey(b => b.UserId);

            // Specify the table name if it differs from the default
            modelBuilder.Entity<User>()
                .ToTable("User");  // Ensure this matches your actual table name
        }
    }
}
