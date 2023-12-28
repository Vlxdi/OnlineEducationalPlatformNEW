using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineEducationalPlatformNEW.Models;

namespace OnlineEducationalPlatformNEW.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
