using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CineEase2.Models;

namespace CineEase2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CineEase2.Models.Actor> Actor { get; set; } = default!;
        public DbSet<CineEase2.Models.Movie> Movie { get; set; } = default!;
        public DbSet<CineEase2.Models.Ticket> Ticket { get; set; } = default!;
    }
}
