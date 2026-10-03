using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Gpuviewer.Models;

namespace Gpuviewer.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<GraphicsCard> GraphicsCards { get; set; }
        //public DbSet<Processor> Processors { get; set; }
    }
}