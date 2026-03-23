using Microsoft.EntityFrameworkCore;
using IDFK.Models;

namespace IDFK.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } // יוצר טבלת משתמשים
        public DbSet<Movie> Movies { get; set; } // יוצר טבלת סרטים
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Actor> Actors { get; set; }


    }
}
