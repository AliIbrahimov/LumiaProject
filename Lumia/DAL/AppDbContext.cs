using Lumia.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lumia.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)   {    }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}
