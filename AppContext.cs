using EFCore.Models;
using Microsoft.EntityFrameworkCore;


namespace EFCore
{
    public class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        public AppContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"DATA SOURCE=DESKTOP-H2M59G7\SQLEXPRESS; DATABASE=EFCore; Trusted_Connection=True; TrustServerCertificate=True;");
        }
    }
}
